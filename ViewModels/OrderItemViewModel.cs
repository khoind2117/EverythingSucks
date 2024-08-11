using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public Guid? SizeId { get; set; }
        public Size? Size { get; set; }

        public Guid? ProductColorId { get; set; }
        public virtual ProductColor? ProductColor { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
