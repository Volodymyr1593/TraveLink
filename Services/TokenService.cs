namespace TraveLink.Servises
{
    public class TokenService : IAccessTokenService
    {

        private readonly IHttpContextAccessor contextAccessor;
       
        




        public TokenService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
           

        }














        public string GenAccessToken(AppUserViewModel model)


        {



            List<Claim> claims = new List<Claim>
             {


             new Claim (JwtRegisteredClaimNames.Name,model.UserName),
              new Claim (JwtRegisteredClaimNames.Email,model.Email)
             };



            SymmetricSecurityKey key = Consts.GetSummetricKey();



            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: Consts.issuer,
           audience: Consts.audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(55),
            signingCredentials

           );
            // this.token = token;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        public bool AccessTokenValid(string CurrentAccesstoken)

        {
            const string bearerPrefix = "Bearer ";
            if (CurrentAccesstoken.StartsWith(bearerPrefix))
            {
                CurrentAccesstoken = CurrentAccesstoken.Substring(bearerPrefix.Length);
            }




            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Consts.issuer,
                ValidateAudience = true,
                ValidAudience = Consts.audience,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Consts.GetSummetricKey()
            };

            
            

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(CurrentAccesstoken, validationParameters, out validatedToken);
             

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        


        public TimeSpan AccessTokenLiveTime(string CurrenAccessttoken)
        {
            const string bearerPrefix = "Bearer ";
            if (CurrenAccessttoken.StartsWith(bearerPrefix))
            {
                // Видалення префікса "Bearer ".
                CurrenAccessttoken = CurrenAccessttoken.Substring(bearerPrefix.Length);
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(CurrenAccessttoken);
            DateTime now = DateTime.UtcNow;

            // Отримати дату закінчення токена.
            DateTime tokenExpiration = decodedToken.ValidTo;

            // Визначити різницю між поточним часом і датою закінчення токена.
            TimeSpan timeRemaining = tokenExpiration - now;

            return timeRemaining;







        }

        

    }
}
