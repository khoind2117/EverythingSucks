namespace EverythingSucks.ViewModels
{
    public class CartItemUpdateViewModel
    {
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
