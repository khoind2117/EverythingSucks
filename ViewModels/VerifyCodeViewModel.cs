using System.ComponentModel.DataAnnotations;

namespace EverythingSucks.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        [Display(Name = "Verification Code")]
        public string Code { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
