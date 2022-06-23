using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Domain.Entities
{
    public class ContactMessage
    {
        [Key]
        public Guid MessageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
