using Microsoft.EntityFrameworkCore;
using Restaurant_API.Data;
using Restaurant_API.Model.Domain;

namespace Restaurant_API.Repositories.RSeat
{
    public class SqlSeatRepository : ISeatRepository
    {
        private readonly RestaurantDbContext dbContext;

        public SqlSeatRepository(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Seat> CreateAsync(Seat seat)
        {
            seat.Id = Guid.NewGuid();
            seat.LastUpdated = DateTime.UtcNow;

            dbContext.Seats.Add(seat);
            await dbContext.SaveChangesAsync();
            return seat;
        }

        public async Task<Seat?> DeleteAsync(Guid id)
        {
            var seat = await dbContext.Seats.FirstOrDefaultAsync(x => x.Id == id);

            if (seat == null)
            {
                return null;
            }

            dbContext.Seats.Remove(seat);
            await dbContext.SaveChangesAsync();
            return seat;
        }

        public async Task<List<Seat>> GetAllAsync()
        {
            return await dbContext.Seats.ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(Guid id)
        {
            return await dbContext.Seats.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Seat?> GetByTableNumberAsync(string tableNumber)
        {
            return await dbContext.Seats.FirstOrDefaultAsync(s => s.TableNumber == tableNumber);
        }

        public async Task<Seat?> UpdateAsync(Guid id, Seat seat)
        {
            var existingSeat = await dbContext.Seats.FirstOrDefaultAsync(x => x.Id == id);

            if (existingSeat == null)
            {
                return null;
            }

            existingSeat.TableNumber = seat.TableNumber;
            existingSeat.Capacity = seat.Capacity;
            existingSeat.Notes = seat.Notes;
            existingSeat.IsReserved = seat.IsReserved;
            existingSeat.IsOccupied = seat.IsOccupied;
            existingSeat.LastUpdated = DateTime.UtcNow;
            existingSeat.Location = seat.Location;
            existingSeat.ReservationTime = seat.ReservationTime;
            existingSeat.DessertId = seat.DessertId;
            existingSeat.MealId = seat.MealId;
            existingSeat.DrinkId = seat.DrinkId;

            await dbContext.SaveChangesAsync();
            return existingSeat;
        }
    }
}
