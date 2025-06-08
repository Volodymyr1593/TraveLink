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
        public virtual DbSet<AppUserViewModel> aspnetusers { get; set; }
        public virtual DbSet<UserRefreshToken> aspnetusertokens{ get; set;}
        public virtual DbSet<HotelViewModel> hotels { get; set; }
        public virtual DbSet<BookingModel> booking { get; set; }    
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureRefreshToken(modelBuilder);
            ConfigureBooking(modelBuilder);

           
        }

        private void ConfigureBooking(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<BookingModel>()
                .HasOne<AppUserViewModel>()
                .WithMany(u => u.Bookings)
                .HasForeignKey (b => b.UserId); 
        }

        private void ConfigureRefreshToken(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRefreshToken>()
               .HasKey(t => new { t.LoginProvider, t.UserId, t.Name });

        }









    }



}
