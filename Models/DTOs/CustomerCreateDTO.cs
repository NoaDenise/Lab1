using System.ComponentModel.DataAnnotations;

namespace Lab1.Models.DTOs
{
    public class CustomerCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; } 
    }
}
