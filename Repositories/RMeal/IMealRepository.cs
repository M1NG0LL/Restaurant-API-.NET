using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RMeal
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllAsync();
        Task<Meal?> GetByIdAsync(Guid id);
        Task<Meal> CreateAsync(Meal meal);
        Task<Meal?> UpdateAsync(Guid id, Meal meal);
        Task<Meal?> DeleteAsync(Guid id);
    }
}
