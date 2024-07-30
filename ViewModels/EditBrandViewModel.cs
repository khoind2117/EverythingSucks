using EverythingSucks.Models;

namespace EverythingSucks.ViewModels
{
    public class EditBrandViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<Product>? AvailableProducts { get; set; }
    }
}