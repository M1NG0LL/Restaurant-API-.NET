using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Model.Domain;
using Restaurant_API.Model.DTO.Dessert;
using Restaurant_API.Model.DTO.Seat;
using Restaurant_API.Repositories.RDessert;
using Restaurant_API.Repositories.RSeat;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly SqlSeatRepository sqlSeatRepository;

        public SeatController(IMapper mapper, SqlSeatRepository sqlSeatRepository)
        {
            this.mapper = mapper;
            this.sqlSeatRepository = sqlSeatRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeats()
        {
            var seats = await sqlSeatRepository.GetAllAsync();
            var seatDtos = mapper.Map<List<SeatDto>>(seats);
            return Ok(seatDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatById(Guid id)
        {
            var seat = await sqlSeatRepository.GetByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found" );
            }

            var seatDto = mapper.Map<SeatDto>(seat);
            return Ok(seatDto);
        }

        [HttpGet("table/{tableNumber}")]
        public async Task<IActionResult> GetSeatByTableNumber(string tableNumber)
        {
            var seat = await sqlSeatRepository.GetByTableNumberAsync(tableNumber);

            if (seat == null)
            {
                return NotFound("Seat not found");
            }

            var seatDto = mapper.Map<SeatDto>(seat);
            return Ok(seatDto);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableSeats()
        {
            var allSeats = await sqlSeatRepository.GetAllAsync();
            var availableSeats = allSeats.Where(s => !s.IsOccupied && !s.IsReserved).ToList();

            var seatDtos = mapper.Map<List<SeatDto>>(availableSeats);
            return Ok(seatDtos);
        }

        [HttpPost("reserve/{id}")]
        public async Task<IActionResult> ReserveSeat(Guid id, [FromBody] DateTime reservationTime)
        {
            var seat = await sqlSeatRepository.GetByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found");
            }

            seat.IsReserved = true;
            seat.ReservationTime = reservationTime;

            await sqlSeatRepository.UpdateAsync(id, seat);

            return Ok(seat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeat([FromBody] CreateSeatDto createSeatDto)
        {
            if (createSeatDto == null)
            {
                return BadRequest("Invalid seat data");
            }

            var existingSeat = await sqlSeatRepository.GetByTableNumberAsync(createSeatDto.TableNumber);
            if (existingSeat != null && existingSeat.IsOccupied)
            {
                return BadRequest("Seat with this table number is already occupied");
            }

            var seat = mapper.Map<Seat>(createSeatDto);
            seat.Id = Guid.NewGuid();
            seat.IsOccupied = true;

            // Sending a link or a QR in email so when they come to the rest they check in and ReserveSeat

            var createdSeat = await sqlSeatRepository.CreateAsync(seat);
            var seatDto = mapper.Map<SeatDto>(createdSeat);

            return Ok(seatDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(Guid id, [FromBody] UpdateSeatDto updateSeat)
        {
            var existingSeat = await sqlSeatRepository.GetByIdAsync(id);

            if (existingSeat == null)
            {
                return NotFound("Seat Not found");
            }

            mapper.Map(updateSeat, existingSeat);
            var updatedSeat = await sqlSeatRepository.UpdateAsync(id, existingSeat);

            var SeatDto = mapper.Map<SeatDto>(updatedSeat);
            return Ok(SeatDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ClearSeat(Guid id)
        {
            var seat = await sqlSeatRepository.GetByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found");
            }

            seat.IsReserved = false;
            seat.IsOccupied = false;
            seat.ReservationTime = null;

            var updatedSeat = await sqlSeatRepository.UpdateAsync(id, seat);

            return Ok(updatedSeat);
        }
    }
}
