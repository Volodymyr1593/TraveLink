
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TraveLink.Controllers;

namespace TraveLink.Tests
{
    public class IdentityControllerTest
    {
       
        [Fact]
        
        public async Task Registration_ReturnsRedirect_WhenUserIsValid()
        {
            // Arrange

        



            var user = new AppUserViewModel


            {  
                UserName ="Test",
                Email ="vov4ik156@gmail.com",
                FirstName ="Flin",
                LastName = "Pirs",
                Password = "securepass"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;




            var Token = new TokenService();
            var Refresh = new RefreshTokenService();
            var UserService = new UserService();
            var context = new ApplicationDbContext(options);
         

            var controller = new IdentityController(context,Token,Refresh,UserService);

            
            var result = await controller.Registration(user);

       
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Showuser", redirect.ActionName);


            var savedUser = await context.aspnetusers.FirstOrDefaultAsync(u => u.UserName == "Test");
            Assert.NotNull(savedUser);
            Assert.Equal("vov4ik156@gmail.com", savedUser.Email);
        }


        


























    }


}
