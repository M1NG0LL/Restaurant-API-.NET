using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RSeat
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetAllAsync();
        Task<Seat?> GetByIdAsync(Guid id);
        Task<Seat?> GetByTableNumberAsync(string tableNumber);
        Task<Seat> CreateAsync(Seat seat);
        Task<Seat?> UpdateAsync(Guid id, Seat seat);
        Task<Seat?> DeleteAsync(Guid id);
    }
}
