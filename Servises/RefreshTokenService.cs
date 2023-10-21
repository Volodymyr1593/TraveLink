namespace NewMVCProjekt.Servises
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ApplicationDbContext context;
        private readonly IAccessTokenService accessTokenService;
        public RefreshTokenService(IHttpContextAccessor contextAccessor, ApplicationDbContext context, IAccessTokenService accessTokenService)
        {
            this.contextAccessor = contextAccessor;
            this.context = context;
            this.accessTokenService = accessTokenService;
        }

        public string GenRefreshToken(AppUserViewModel model)
        {

            List<Claim> claims = new List<Claim>
             {


             new Claim (JwtRegisteredClaimNames.Name,model.UserName),
              new Claim (JwtRegisteredClaimNames.Email,model.Email)
             };



            SymmetricSecurityKey key = Consts.GetRefreshSummetricKey();



            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: Consts.issuer,
           audience: Consts.audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(Consts.RefreshTokenExpirationHours),
            signingCredentials

           );
            // this.token = token;
            return new JwtSecurityTokenHandler().WriteToken(token);








        }

        public bool RefreshTokenValid(string CurrenRefreshttoken)

        {





            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Consts.issuer,
                ValidateAudience = true,
                ValidAudience = Consts.audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Consts.GetRefreshSummetricKey()
            };

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(CurrenRefreshttoken, validationParameters, out validatedToken);


                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public string GetRefreshToken(string CurrentAccesstoken)
        {
            const string bearerPrefix = "Bearer ";
            if (CurrentAccesstoken.StartsWith(bearerPrefix))
            {
                // Видалення префікса "Bearer ".
                CurrentAccesstoken = CurrentAccesstoken.Substring(bearerPrefix.Length);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(CurrentAccesstoken);
            var claims = decodedToken.Claims.ToList();
            var claim = claims.First();
            var username = claim.Value;
            var user =  context.aspnetusers.FirstOrDefault(u => u.UserName == username);


            var RefreshTokenData =  context.aspnetusertokens.FirstOrDefault(u => u.UserId == user.Id.ToString());
            var RefreshToken = RefreshTokenData?.Value;

            return RefreshToken;

        }

        public void DeleteRefreshToken(string CurrentRefreshtoken)
        {

            var refreshTokenEntity =  context.UserTokens.FirstOrDefault(t => t.Value == CurrentRefreshtoken);


            if (refreshTokenEntity != null)
            {
                context.UserTokens.Remove(refreshTokenEntity);
                 context.SaveChanges();
            }












        }

        public async Task RefreshAccessToken(string CurrentAccesstoken)


        {
            const string bearerPrefix = "Bearer ";
            if (CurrentAccesstoken.StartsWith(bearerPrefix))
            {
                // Видалення префікса "Bearer ".
                CurrentAccesstoken = CurrentAccesstoken.Substring(bearerPrefix.Length);
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(CurrentAccesstoken);
            var claims = decodedToken.Claims.ToList();
            var claim = claims.First();
            var username = claim.Value;
            var user = await context.aspnetusers.FirstOrDefaultAsync(u => u.UserName == username);



            var Usertoken = accessTokenService.GenAccessToken(user);

            string authorizationHeader = "Bearer " + Usertoken;

           contextAccessor.HttpContext?.Session.Remove("AuthToken");

            contextAccessor.HttpContext?.Session.SetString("AuthToken", authorizationHeader);











        }
    }



}
