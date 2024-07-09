using CloudinaryDotNet.Actions;

namespace EverythingSucks.Models
{
    public class OrderStatus
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
