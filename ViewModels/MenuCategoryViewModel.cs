namespace EverythingSucks.ViewModels
{
    public class MenuCategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
        public List<ProductTypeViewModel> ProductTypes { get; set; }
    }
}
