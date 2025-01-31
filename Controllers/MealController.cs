using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Model.Domain;
using Restaurant_API.Model.DTO.Meal;
using Restaurant_API.Repositories.RMeal;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly SqlMealRepository sqlMealRepository;

        public MealController(IMapper mapper, SqlMealRepository sqlMealRepository)
        {
            this.mapper = mapper;
            this.sqlMealRepository = sqlMealRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meals = await sqlMealRepository.GetAllAsync();
            var mealDtos = mapper.Map<List<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMealById(Guid id)
        {
            var meal = await sqlMealRepository.GetByIdAsync(id);
            if (meal == null)
            {
                return NotFound("Meal Not Found");
            }

            var mealDto = mapper.Map<MealDto>(meal);
            return Ok(mealDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMeal([FromBody] CreateMealDto mealDto)
        {
            var MealDomainModel = mapper.Map<Meal>(mealDto);
            var NewMeal = await sqlMealRepository.CreateAsync(MealDomainModel);

            var MealDto = mapper.Map<MealDto>(NewMeal);

            return Ok(MealDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMeal(Guid id, [FromBody] UpdateMealDto updatemealDto)
        {
            var existingMeal = await sqlMealRepository.GetByIdAsync(id);
            if (existingMeal == null)
            {
                return NotFound("Meal Not found");
            }

            mapper.Map(updatemealDto, existingMeal);
            var updatedMeal = await sqlMealRepository.UpdateAsync(id, existingMeal);

            var mealDto = mapper.Map<MealDto>(updatedMeal);
            return Ok(mealDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            var exisitingMeal = await sqlMealRepository.DeleteAsync(id);

            if (exisitingMeal == null)
            {
                return NotFound("Meal Not found");
            }

            return Ok("Meal is deleted");
        }

    }
}
