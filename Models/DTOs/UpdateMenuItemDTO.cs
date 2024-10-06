namespace Lab1.Models.DTOs
{
    public class UpdateMenuItemDTO
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
    }
}
