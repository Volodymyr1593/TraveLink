using Microsoft.AspNetCore.Mvc;
using TraveLink.Models;

namespace TraveLink.Controllers
{
    
    public class HotelsController : Controller
    { 
        public HotelsController(ApplicationDbContext context)
        {
            this.context = context; 
        }
        public readonly ApplicationDbContext context;
        public async Task <IActionResult> Index( int? id, HotelViewModel model)
        {

           
            model = await context.hotels.FindAsync(id);

            

            
            return View(model);



        }











    }
}
