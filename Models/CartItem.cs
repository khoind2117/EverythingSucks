using System.Text.Json.Serialization;

namespace EverythingSucks.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }

        public Guid? SizeId { get; set; }
        public Size? Size { get; set; }

        public Guid? ProductColorId { get; set; }
        public virtual ProductColor? ProductColor { get; set; }

        public Guid CartId { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}