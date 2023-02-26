using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class AdminViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is requierd")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is requierd")]
        public string LastName { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is requierd")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is requierd")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
        public List<AppUserModel>? Users { get; set; }

    }
}
