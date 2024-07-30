using System.ComponentModel.DataAnnotations;

namespace EverythingSucks.ViewModels
{
    public class ColorSelection
    {
        [Required(ErrorMessage = "Color is required")]
        public Guid ColorId { get; set; }
        [Required(ErrorMessage = "Primary image is required")]
        public IFormFile PrimaryImage { get; set; }
        public List<IFormFile> AdditionalImages { get; set; } = new List<IFormFile>();
    }
}
