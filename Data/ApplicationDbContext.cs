using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NewMVCProjekt.Models;
namespace NewMVCProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserViewModel>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<AppUserViewModel> aspnetusers { get; set; }
        
       
    }



}
