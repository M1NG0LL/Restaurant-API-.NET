using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Model.Domain;
using Restaurant_API.Model.DTO.Drink;
using Restaurant_API.Repositories.RDrink;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly SqlDrinkRepository sqlDrinkRepository;

        public DrinkController(IMapper mapper, SqlDrinkRepository sqlDrinkRepository)
        {
            this.mapper = mapper;
            this.sqlDrinkRepository = sqlDrinkRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDrink([FromBody] CreateDrinkDto createDrinkDto)
        {
            var drinkDomainModel = mapper.Map<Drink>(createDrinkDto);
            var newDrink = await sqlDrinkRepository.CreateAsync(drinkDomainModel);
            var drinkDto = mapper.Map<DrinkDto>(newDrink);

            return Ok(drinkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrinks()
        {
            var drinks = await sqlDrinkRepository.GetAllAsync();
            var drinksDto = mapper.Map<List<DrinkDto>>(drinks);

            return Ok(drinksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrinkById(Guid id)
        {
            var drink = await sqlDrinkRepository.GetByIdAsync(id);

            if (drink == null)
            {
                return NotFound("Drink Not Found");
            }

            var drinkDto = mapper.Map<DrinkDto>(drink);
            return Ok(drinkDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDrink(Guid id, [FromBody] UpdateDrinkDto updateDrinkDto)
        {
            var existingDrink = await sqlDrinkRepository.GetByIdAsync(id);
            if (existingDrink == null)
            {
                return NotFound("Drink Not Found");
            }

            mapper.Map(updateDrinkDto, existingDrink);
            var updatedDrink = await sqlDrinkRepository.UpdateAsync(id, existingDrink);

            var drinkDto = mapper.Map<DrinkDto>(updatedDrink);
            return Ok(drinkDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDrink(Guid id)
        {
            var drink = await sqlDrinkRepository.DeleteAsync(id);

            if (drink == null)
            {
                return NotFound("Drink Not Found");
            }

            return Ok(drink);
        }
    }
}
