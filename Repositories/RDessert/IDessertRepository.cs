using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RDessert
{
    public interface IDessertRepository
    {
        Task<List<Dessert>> GetAllAsync();
        Task<Dessert?> GetByIdAsync(Guid id);
        Task<Dessert> CreateAsync(Dessert dessert);
        Task<Dessert?> UpdateAsync(Guid id, Dessert dessert);
        Task<Dessert?> DeleteAsync(Guid id);
    }
}
