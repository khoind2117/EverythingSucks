namespace EverythingSucks.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public List<ProductColorViewModel> ProductColors { get; set; }
    }

}
