namespace TraveLink.Types
{
    public interface IAdminService
    {

        public  Task<List<AppUserViewModel>> GetAllUsers();


    }
}
