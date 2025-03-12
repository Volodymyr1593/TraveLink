namespace TraveLink.Types
{
    public interface IUserService

    {
        public AppUserViewModel GetUserInform();
        public SearchReserveModel GetSearchInform();
        public Task SaveOrder(AppUserViewModel model);
        public Task<AppUserViewModel> GetOrder(AppUserViewModel model);








    }
}
