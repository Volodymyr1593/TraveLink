namespace NewMVCProjekt.Types
{
    public interface IRefreshTokenService
    {



        public bool RefreshTokenValid(string CurrentRefreshtoken);
        public string GenRefreshToken(AppUserViewModel model);
        public string GetRefreshToken(string CurrentAccesstoken);
        public void DeleteRefreshToken(string CurrentRefreshtoken);

        public Task RefreshAccessToken(string CurrentRefreshtoken);


    }
}
