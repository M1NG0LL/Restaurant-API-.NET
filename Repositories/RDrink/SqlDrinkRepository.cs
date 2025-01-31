using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RDrink
{
    public class SqlDrinkRepository : IDrinkRepository
    {
        private readonly RestaurantDbContext dbContext;

        public SqlDrinkRepository(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Drink> CreateAsync(Drink drink)
        {
            drink.Id = Guid.NewGuid();
            dbContext.Drinks.Add(drink);
            await dbContext.SaveChangesAsync();
            return drink;
        }

        public async Task<Drink?> DeleteAsync(Guid id)
        {
            var drink = dbContext.Drinks.FirstOrDefault(drink => drink.Id == id);
            if (drink == null)
            {
                return null;
            }

            dbContext.Drinks.Remove(drink);
            await dbContext.SaveChangesAsync();
            return drink;
        }

        public async Task<List<Drink>> GetAllAsync()
        {
            return await dbContext.Drinks.ToListAsync();
        }

        public async Task<Drink?> GetByIdAsync(Guid id)
        {
            return await dbContext.Drinks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Drink?> UpdateAsync(Guid id, Drink drink)
        {
            var existingDrink = await dbContext.Drinks.FirstOrDefaultAsync(drink => drink.Id == id);
            if (existingDrink == null)
            {
                return null;
            }

            existingDrink.Name = drink.Name;
            existingDrink.Description = drink.Description;
            existingDrink.Price = drink.Price;
            existingDrink.IsAvailable = drink.IsAvailable;
            existingDrink.VolumeInMl = drink.VolumeInMl;

            await dbContext.SaveChangesAsync();
            return existingDrink;
        }
    }
}
