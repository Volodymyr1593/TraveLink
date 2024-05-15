namespace TraveLink.Types
{
    public interface IAccessTokenService
    {

        public string GenAccessToken( AppUserViewModel model);

       

        public bool AccessTokenValid(string CurrentAccesstoken);

       
        public TimeSpan AccessTokenLiveTime (string CurrentAccesstoken);

       
      
       

        




    }

}
