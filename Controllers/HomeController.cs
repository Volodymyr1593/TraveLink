using Microsoft.AspNetCore.Mvc;
using TraveLink.Models;
using System.Diagnostics;
using TraveLink.Data;
using Microsoft.EntityFrameworkCore;

namespace TraveLink.Controllers
{
       public class HomeController : Controller

    {
        private readonly ILogger<HomeController> logger;

        private readonly HotelListingService hotelList;

        public HomeController(ILogger<HomeController> logger, HotelListingService hotelList)
        {
            this.logger = logger;
            this.hotelList = hotelList;
        }









        public async Task <IActionResult> Index(int? page)
        {
            var _page = page ?? 1;
            var Totalpage = await hotelList.GetTotalHotels();

            ViewData["Totalpage"] = Totalpage;

            
            if (_page < 1)
            {
                _page = 1;
            }


            var path = HttpContext.Request.Path;


            if (path == "/Identity/RefreshToken")
            {

               string message = "SessionExpiredMessage. Your session has expired. Please log in again.";

                // Зберегти оповіщення в TempData
                 TempData["Message"] = message;
                // //

                // Очистити оповіщення з TempData, щоб воно не було доступне в наступному запиті
                //TempData.Remove("Message");


                return View();
            }
            else
            {
                List<HotelViewModel> list = await hotelList.GetHotelViewModels(_page); 

                return View(list);

            }
           


        }


      
      




      


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}