using Microsoft.AspNetCore.Mvc;
using NewMVCProjekt.Models;

namespace NewMVCProjekt.Controllers
{
    public class HotelsController : Controller
    {


        public IActionResult Index()
        {

            HotelViewModel Redison = new HotelViewModel()
            { Id = 3, Name = "REDISON" };




            return View(Redison);



        }











    }
}
