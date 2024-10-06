namespace Lab1.Models.DTOs
{
    public class ReservationCreateDTO
    {
        public int? CustomerId { get; set; } // Om kunden redan finns
        public string Name { get; set; } // För nya kunder
        public string Email { get; set; } // För nya kunder
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public TimeSpan ReservationTime { get; set; } // Nytt fält för bokningstid
    }
}
