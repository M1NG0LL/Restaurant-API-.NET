using System.ComponentModel.DataAnnotations;

namespace Restaurant_API.Model.Domain
{
    public class Seat
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TableNumber { get; set; }

        [Required]
        [Range(1, 20)]
        public int Capacity { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public bool IsOccupied { get; set; }

        [Required]
        public bool IsReserved { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public DateTime? ReservationTime { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

        // ===================================

        // Foreign keys
        public Guid DessertId { get; set; }
        public Guid MealId { get; set; }
        public Guid DrinkId { get; set; }

        // Navigation properties
        public Dessert Dessert { get; set; }

        public Meal Meal { get; set; }

        public Drink Drink { get; set; }
    }
}
