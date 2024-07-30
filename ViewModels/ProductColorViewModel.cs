namespace EverythingSucks.ViewModels
{
    public class ProductColorViewModel
    {
        public Guid? ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }
    }

}
