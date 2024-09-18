namespace Lab1.Models.DTOs
{
    public class MenuItemDTO
    {
        public int MenuItemId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
