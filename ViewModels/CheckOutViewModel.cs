using System.ComponentModel.DataAnnotations;

namespace EverythingSucks.ViewModels
{
    public class CheckOutViewModel
    {
        public bool IsPersonalInfo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
