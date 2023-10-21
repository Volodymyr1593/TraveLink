namespace NewMVCProjekt
{
    public static class Consts
    {
      

        public const string issuer = " http://localhost:40217 ";

        public const string audience = issuer;

        public const string SecretKey = "This is secret key for jwt token in this projekt";

        public const string SecretRefreshKey = "This is secret key for refresh token in this projekt";

        public const int RefreshTokenExpirationHours  = 7;
        public static  SymmetricSecurityKey GetSummetricKey()
        

            =>new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public static SymmetricSecurityKey GetRefreshSummetricKey()


          => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretRefreshKey));

















    }





}
