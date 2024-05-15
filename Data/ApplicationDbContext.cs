using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TraveLink.Models;
namespace TraveLink.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUserViewModel>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AppUserViewModel> aspnetusers { get; set; }
        public DbSet<UserRefreshToken> aspnetusertokens{ get; set;}
        public DbSet<HotelViewModel> hotels { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRefreshToken>()
                .HasKey(t => new { t.LoginProvider, t.UserId,t.Name });
        }
    }



}
