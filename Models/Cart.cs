using System.Text.Json.Serialization;

namespace EverythingSucks.Models
{
    public class Cart
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ProductCount { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? CartStatusId { get; set; }
        public CartStatus? CartStatus { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }

        // Constructor khởi tạo CartItems
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}