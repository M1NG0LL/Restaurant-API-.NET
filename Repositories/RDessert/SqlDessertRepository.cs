using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RDessert
{
    public class SqlDessertRepository : IDessertRepository
    {
        private readonly RestaurantDbContext dbContext;

        public SqlDessertRepository(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Dessert> CreateAsync(Dessert dessert)
        {
            dessert.Id = Guid.NewGuid();
            dbContext.Desserts.Add(dessert);
            await dbContext.SaveChangesAsync();
            return dessert;
        }

        public async Task<Dessert?> DeleteAsync(Guid id)
        {
            var dessert = await dbContext.Desserts.FirstOrDefaultAsync(x => x.Id == id);
            if (dessert == null)
            {
                return null;
            }

            dbContext.Desserts.Remove(dessert);
            await dbContext.SaveChangesAsync();
            return dessert;
        }

        public async Task<List<Dessert>> GetAllAsync()
        {
            return await dbContext.Desserts.ToListAsync();
        }

        public async Task<Dessert?> GetByIdAsync(Guid id)
        {
            return await dbContext.Desserts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Dessert?> UpdateAsync(Guid id, Dessert dessert)
        {
            var existingDessert = await dbContext.Desserts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDessert == null)
            {
                return null;
            }

            existingDessert.Name = dessert.Name;
            existingDessert.Description = dessert.Description;
            existingDessert.Price = dessert.Price;
            existingDessert.IsAvailable = dessert.IsAvailable;
            existingDessert.Category = dessert.Category;
            existingDessert.PreparationTimeInMin = dessert.PreparationTimeInMin;

            await dbContext.SaveChangesAsync();
            return existingDessert;
        }
    }
}
