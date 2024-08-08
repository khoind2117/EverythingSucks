using EverythingSucks.Models;
using EverythingSucks.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EverythingSucks.ViewModels
{
    public class EditProductViewModel
    {
        // Các thuộc tính sản phẩm
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description length can't be more than 500.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product Type is required")]
        public Guid? ProductTypeId { get; set; }

        // Màu và hình ảnh của sản phẩm
        [ValidateColorSelections(ErrorMessage = "At least one color selection is required.")]
        public List<ColorSelection> ColorSelections { get; set; }

        // Các danh sách để hiển thị trong form
        public List<Category>? AvailableCategories { get; set; }
        public List<Color>? AvailableColors { get; set; }
    }
}
