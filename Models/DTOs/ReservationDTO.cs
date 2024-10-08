﻿namespace Lab1.Models.DTOs
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int? TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public TimeSpan ReservationTime { get; set; }
    }
}
