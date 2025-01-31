using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RDrink
{
    public interface IDrinkRepository
    {
        Task<List<Drink>> GetAllAsync();
        Task<Drink?> GetByIdAsync(Guid id);
        Task<Drink> CreateAsync(Drink drink);
        Task<Drink?> UpdateAsync(Guid id, Drink drink);
        Task<Drink?> DeleteAsync(Guid id);
    }
}
