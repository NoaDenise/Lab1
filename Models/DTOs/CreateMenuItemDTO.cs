namespace Lab1.Models.DTOs
{
    public class CreateMenuItemDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }  
    }
}
