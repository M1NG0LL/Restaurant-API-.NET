namespace Restaurant_API.Model.DTO.Seat
{
    public class SeatDto
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsReserved { get; set; }
        public string Location { get; set; }
        public DateTime? ReservationTime { get; set; }
        public string Notes { get; set; }
        public Guid DessertId { get; set; }
        public Guid MealId { get; set; }
        public Guid DrinkId { get; set; }
    }
}
