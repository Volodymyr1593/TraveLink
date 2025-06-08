using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraveLink.Tests
{    public class TokenServiceTest1
    {
        [Fact]
        public void GenAccessTokenTest()
        {
            var user = new AppUserViewModel()
            {
                Email = "vov4ik155@mail.com",
                UserName = "Pirs"
            };

            var tokenService = new TokenService();
            var tokenString = tokenService.GenAccessToken(user);

            
            Assert.False(string.IsNullOrEmpty(tokenString));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);


            Assert.Contains(token.Claims, c => c.Type == JwtRegisteredClaimNames.Name && c.Value == user.UserName);
            Assert.Contains(token.Claims, c => c.Type == JwtRegisteredClaimNames.Email && c.Value == user.Email);

            Assert.Equal(Consts.issuer, token.Issuer);
            Assert.Equal(Consts.audience, token.Audiences.FirstOrDefault());

        }















    }
}
