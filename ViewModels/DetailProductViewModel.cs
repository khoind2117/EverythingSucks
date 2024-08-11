using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class DetailProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductSlug { get; set; }
        public DateTime ProductCreatedAt { get; set; }
        public DateTime ProductUpdatedAt { get; set; }
        public List<ProductColorViewModel> ProductColors { get; set; }
        public List<Size> Sizes { get; set; }
    }
}
