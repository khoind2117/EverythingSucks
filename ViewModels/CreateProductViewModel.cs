using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? ProductTypeId { get; set; }

        public List<Brand>? AvailableBrands { get; set; }
        public List<Category>? AvailableCategories { get; set; }
        public List<Color>? AvailableColors { get; set; }
        public List<Guid>? SelectedColorIds { get; set; }
    }
}
