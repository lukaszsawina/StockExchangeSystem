using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class AppUserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
