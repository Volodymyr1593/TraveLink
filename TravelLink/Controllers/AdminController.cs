using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TraveLink.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;




        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]





        public async Task<ActionResult> Index()
        {



            var userList = await _adminService.GetAllUsers();

            return View(userList);
        }

       




    }
}
