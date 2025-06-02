namespace TraveLink.Services
{
    public class AdminService:IAdminService
    {
        public readonly ApplicationDbContext context;
        public AdminService(ApplicationDbContext context)
        {
            this.context = context; 
        }

        public async Task< List<AppUserViewModel>> GetAllUsers()
        {


            var userList =  await context.aspnetusers.ToListAsync();
            if(userList != null)
            {
                return userList;

            }
            else
            {
                return new List<AppUserViewModel> ();
            }




        }


    }

}
