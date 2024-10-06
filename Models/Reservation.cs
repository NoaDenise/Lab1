using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int? TableId { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }
        [Required]
        public TimeSpan ReservationTime { get; set; }
    }
}
