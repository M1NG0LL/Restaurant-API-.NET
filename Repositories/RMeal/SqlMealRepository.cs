using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RMeal
{
    public class SqlMealRepository : IMealRepository
    {
        private readonly RestaurantDbContext dbContext;

        public SqlMealRepository(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Meal> CreateAsync(Meal meal)
        {
            meal.Id = Guid.NewGuid();

            await dbContext.AddAsync(meal);
            await dbContext.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal?> DeleteAsync(Guid id)
        {
            var existingMeal = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMeal == null)
            {
                return null;
            }

            dbContext.Meals.Remove(existingMeal);
            await dbContext.SaveChangesAsync();

            return existingMeal;
        }

        public async Task<List<Meal>> GetAllAsync()
        {
            return await dbContext.Meals.ToListAsync();
        }

        public async Task<Meal?> GetByIdAsync(Guid id)
        {
            var existingMeal = dbContext.Meals.FirstOrDefault(x => x.Id == id);

            if (existingMeal == null)
            {
                return null;
            }

            return existingMeal;
        }

        public async Task<Meal?> UpdateAsync(Guid id, Meal meal)
        {
            var existingMeal = await dbContext.Meals.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMeal == null)
            {
                return null;
            }

            existingMeal.Name = meal.Name;
            existingMeal.Description = meal.Description;
            existingMeal.Price = meal.Price;
            existingMeal.Category = meal.Category;
            existingMeal.PreparationTimeInMin = meal.PreparationTimeInMin;

            await dbContext.SaveChangesAsync();
            return existingMeal;
        }
    }
}
