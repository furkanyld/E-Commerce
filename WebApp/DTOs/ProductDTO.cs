namespace WebApp.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
    }
}
