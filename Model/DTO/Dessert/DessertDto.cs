namespace Restaurant_API.Model.DTO.Dessert
{
    public class DessertDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Category { get; set; }
        public int PreparationTimeInMin { get; set; }
    }
}
