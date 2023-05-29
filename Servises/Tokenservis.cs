namespace NewMVCProjekt.Servises
{
    public class Tokenservis : IAutenticnterface
    {





        public JwtSecurityToken? token;







        public string GetToken(AppUserViewModel model)

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
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials


           );

            this.token = token;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

            
       









    }
   

}
