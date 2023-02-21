using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is requierd")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is requierd")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is requierd")]
        public string Email { get; set; }
    }
}
