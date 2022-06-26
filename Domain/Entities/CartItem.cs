

namespace Pharmacy.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
