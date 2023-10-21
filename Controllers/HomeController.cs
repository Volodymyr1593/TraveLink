using Microsoft.AspNetCore.Mvc;
using NewMVCProjekt.Models;
using System.Diagnostics;
using NewMVCProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace NewMVCProjekt.Controllers
{
       public class HomeController : Controller

    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }   








        
        public IActionResult Index()
        {
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

            return View();


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