namespace Lab1.Models.DTOs
{
    public class ReservationCreateDTO
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
