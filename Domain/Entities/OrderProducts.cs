namespace Pharmacy.Domain.Entities
{
    public class OrderProducts
    {
        public Guid OrderProductId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
