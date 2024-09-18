using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
