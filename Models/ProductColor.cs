using System.Text.Json.Serialization;

namespace EverythingSucks.Models
{
    public class ProductColor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid? ColorId { get; set; }
        public virtual Color? Color { get; set; }

        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}