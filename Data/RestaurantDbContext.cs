using Microsoft.EntityFrameworkCore;
using Restaurant_API.Model.Domain;

namespace Restaurant_API.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> dbContext) : base(dbContext) { }

        public DbSet<Seat> Seats { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Dessert> Desserts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Meals
            modelBuilder.Entity<Meal>().HasData(
                new Meal { Id = Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), Name = "Grilled Salmon", Description = "Fresh salmon with herbs", Price = 25.99m, Category = "Main Course", PreparationTimeInMin = 25 },
                new Meal { Id = Guid.Parse("b1c2d3e4-f5a6-7890-1234-56789abcdef1"), Name = "Beef Steak", Description = "Premium cut beef steak", Price = 29.99m, Category = "Main Course", PreparationTimeInMin = 20 },
                new Meal { Id = Guid.Parse("c1d2e3f4-a5b6-7890-1234-56789abcdef2"), Name = "Chicken Alfredo", Description = "Creamy pasta with chicken", Price = 18.99m, Category = "Pasta", PreparationTimeInMin = 15 },
                new Meal { Id = Guid.Parse("d1e2f3a4-b5c6-7890-1234-56789abcdef3"), Name = "Vegetarian Pizza", Description = "Fresh vegetables on crispy base", Price = 16.99m, Category = "Pizza", PreparationTimeInMin = 20 },
                new Meal { Id = Guid.Parse("e1f2a3b4-c5d6-7890-1234-56789abcdef4"), Name = "Caesar Salad", Description = "Classic caesar with chicken", Price = 12.99m, Category = "Salad", PreparationTimeInMin = 10 }
            );

            // Seed Drinks
            modelBuilder.Entity<Drink>().HasData(
                new Drink { Id = Guid.Parse("f1a2b3c4-d5e6-7890-1234-56789abcdef5"), Name = "House Wine", Description = "Red wine", Price = 7.99m, IsAvailable = true, VolumeInMl = 175 },
                new Drink { Id = Guid.Parse("a2b3c4d5-e6f7-7890-1234-56789abcdef6"), Name = "Craft Beer", Description = "Local IPA", Price = 5.99m, IsAvailable = true, VolumeInMl = 500 },
                new Drink { Id = Guid.Parse("b2c3d4e5-f6a7-7890-1234-56789abcdef7"), Name = "Fresh Orange Juice", Description = "Freshly squeezed", Price = 3.99m, IsAvailable = true, VolumeInMl = 250 },
                new Drink { Id = Guid.Parse("c2d3e4f5-a6b7-7890-1234-56789abcdef8"), Name = "Sparkling Water", Description = "Mineral water", Price = 2.99m, IsAvailable = true, VolumeInMl = 330 },
                new Drink { Id = Guid.Parse("d2e3f4a5-b6c7-7890-1234-56789abcdef9"), Name = "Espresso", Description = "Single shot", Price = 2.99m, IsAvailable = true, VolumeInMl = 50 }
            );

            // Seed Desserts
            modelBuilder.Entity<Dessert>().HasData(
                new Dessert { Id = Guid.Parse("c4511eba-e269-4566-b56e-2ee3662a6dd2"), Name = "Chocolate Cake", Description = "Rich chocolate layer cake", Price = 8.99m, Category = "Cake", PreparationTimeInMin = 10, IsAvailable = true },
                new Dessert { Id = Guid.Parse("d1e2f3a4-b5c6-7890-1234-56789abcdef3"), Name = "Tiramisu", Description = "Classic Italian dessert", Price = 7.99m, Category = "Italian", PreparationTimeInMin = 5, IsAvailable = true },
                new Dessert { Id = Guid.Parse("414033df-b813-49d1-b1e6-e4722c17aa74"), Name = "Ice Cream", Description = "Vanilla with berries", Price = 5.99m, Category = "Ice Cream", PreparationTimeInMin = 3, IsAvailable = true },
                new Dessert { Id = Guid.Parse("71e1f68e-adf1-451b-ad74-3974891ba91a"), Name = "Apple Pie", Description = "Warm apple pie", Price = 6.99m, Category = "Pie", PreparationTimeInMin = 15, IsAvailable = true },
                new Dessert { Id = Guid.Parse("1d7be41b-5f4d-4398-9e2c-93d113016866"), Name = "Cheesecake", Description = "New York style", Price = 7.99m, Category = "Cake", PreparationTimeInMin = 5, IsAvailable = true }
            );
        }
    }
}
