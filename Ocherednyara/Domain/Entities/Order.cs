namespace Pharmacy.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string Address { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }
    }
}
