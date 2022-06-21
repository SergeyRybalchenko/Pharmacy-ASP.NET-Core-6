namespace Pharmacy.Models
{
    public class StoreSingleProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; } 
    }
}
