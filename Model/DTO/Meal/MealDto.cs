using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_API.Model.DTO.Meal
{
    public class MealDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int PreparationTimeInMin { get; set; }
    }
}
