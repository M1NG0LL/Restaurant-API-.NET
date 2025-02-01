namespace Restaurant_API.Model.DTO.Seat
{
    public class UpdateSeatDto
    {
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public DateTime? ReservationTime { get; set; }
        public string Notes { get; set; }
        public Guid? DessertId { get; set; }
        public Guid? MealId { get; set; }
        public Guid? DrinkId { get; set; }
    }
}
