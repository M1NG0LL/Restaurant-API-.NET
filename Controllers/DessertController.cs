using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Model.Domain;
using Restaurant_API.Model.DTO.Dessert;
using Restaurant_API.Repositories.RDessert;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DessertController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly SqlDessertRepository sqlDessertRepository;

        public DessertController(IMapper mapper, SqlDessertRepository sqlDessertRepository)
        {
            this.mapper = mapper;
            this.sqlDessertRepository = sqlDessertRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var desserts = await sqlDessertRepository.GetAllAsync();
            var dessertsDto = mapper.Map<List<DessertDto>>(desserts);
            return Ok(dessertsDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dessert = await sqlDessertRepository.GetByIdAsync(id);

            if (dessert == null)
            {
                return NotFound("Dessert Not Found");
            }

            var dessertDto = mapper.Map<DessertDto>(dessert);
            return Ok(dessertDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDessertDto createDessertDto)
        {
            var dessert = mapper.Map<Dessert>(createDessertDto);
            var newDessert = await sqlDessertRepository.CreateAsync(dessert);
            var dessertDto = mapper.Map<DessertDto>(newDessert);

            return Ok(dessertDto);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDessertDto updateDessertDto)
        {
            var existingDessert = await sqlDessertRepository.GetByIdAsync(id);
            if (existingDessert == null)
            {
                return NotFound("Dessert Not Found");
            }

            mapper.Map(updateDessertDto, existingDessert);
            var updatedDessert = await sqlDessertRepository.UpdateAsync(id, existingDessert);

            var dessertDto = mapper.Map<DessertDto>(updatedDessert);
            return Ok(dessertDto);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await sqlDessertRepository.DeleteAsync(id);

            if (deleted == null)
            {
                return NotFound("Dessert Not Found");
            }

            return Ok(deleted);
        }
    }
}
