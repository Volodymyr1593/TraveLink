using Microsoft.AspNetCore.Mvc;
using NewMVCProjekt.Models;
using System.Diagnostics;
using NewMVCProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace NewMVCProjekt.Controllers
{
       public class HomeController : Controller

    {
        private readonly ILogger<Authenticationcontroller> logger;

        public HomeController(ILogger<Authenticationcontroller> logger)
        {
            this.logger = logger;
        }   








        // private static List<UserViewModel> Users = new List<UserViewModel>();
        public IActionResult Index()
        {
           return View();
       }

      // public IActionResult SignIn()
      //  {
      //      return View();
       // }

      




      


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