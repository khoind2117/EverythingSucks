namespace EverythingSucks.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }

        public Guid? SizeId { get; set; }
        public Size? Size { get; set; }

        public Guid? ProductColorId { get; set; }
        public virtual ProductColor? ProductColor { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}