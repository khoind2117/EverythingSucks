using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; }

        public Guid? SizeId { get; set; }
        public Size? Size { get; set; }

        public Guid? ProductColorId { get; set; }
        public virtual ProductColor? ProductColor { get; set; }

        // Giỏ hàng không có CartId khi người dùng chưa đăng nhập
    }

}
