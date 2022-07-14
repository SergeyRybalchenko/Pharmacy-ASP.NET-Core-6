namespace Pharmacy.Models
{
    public class AdminProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
