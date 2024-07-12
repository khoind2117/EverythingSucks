using System.ComponentModel.DataAnnotations;

namespace EverythingSucks.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
