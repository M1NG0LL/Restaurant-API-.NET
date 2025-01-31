namespace Restaurant_API.Model.DTO.Drink
{
    public class UpdateDrinkDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsAvailable { get; set; }
        public int? VolumeInMl { get; set; }
    }
}
