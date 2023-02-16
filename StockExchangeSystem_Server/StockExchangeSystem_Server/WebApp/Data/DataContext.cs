using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDataContext : IdentityDbContext<AppUserModel>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }
        
    }
}
