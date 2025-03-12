using Microsoft.AspNetCore.Mvc;
using TraveLink.Models;

namespace TraveLink.Controllers
{
    
    public class HotelsController : Controller
    { 
        public HotelsController(ApplicationDbContext context, HotelParceService hotelParce)
        {
            this.context = context;
            this.hotelParce = hotelParce;   
        }
        public readonly ApplicationDbContext context;
        public readonly HotelParceService hotelParce;



        public async Task <IActionResult> Index( int?id, HotelViewModel model)
        {
            var dataListJson = HttpContext.Session.GetString("dataListJson");
            var sesionHotelId = HttpContext.Session.GetInt32("sesionHotelId");
            List<HotelRoomData>dataList = new List<HotelRoomData>();

            if (!string.IsNullOrEmpty(dataListJson))

            {
                



                 dataList = JsonConvert.DeserializeObject<List<HotelRoomData>>(dataListJson);




            }
               
                
               if(id != null )
            {


                model = await context.hotels.FindAsync(id);
                

            }
            if (dataList!= null&&sesionHotelId==id)
            {


                model.roomDataModels = dataList;

            }



           

            

            
            return View(model);



        }
        
      public async Task<IActionResult> SearchInHotel(int id ,HotelViewModel model)
       {



          




            var html = await  hotelParce.GethtmlHotelDocument(model.url);
            var form =  hotelParce.CreateSearchFormContent(html,model);
            var response = await hotelParce.SentSearchForm(html,form,model);
            if (response != null)
            {
                var dataFromHotel = hotelParce.GetDataTableFromHotel(response);
                var dataListJson = JsonConvert.SerializeObject(dataFromHotel);
                var CurrentOrderForm = JsonConvert.SerializeObject(model.reserveModel);
                if (dataFromHotel != null)
                {

                    HttpContext.Session.SetString("dataListJson", dataListJson);
                    HttpContext.Session.SetInt32("sesionHotelId", id);
                    HttpContext.Session.SetString("CurrentOrderForm",CurrentOrderForm);

                    return RedirectToAction("Index", new { id = id });
                }
                else
                {
                    HttpContext.Session.Remove("sesionHotelId");
                    string message = "there are no available rooms for this date";

                    TempData["RoomMessage"] = message;
                    return RedirectToAction("Index", new { id = id });

                }






            }






            else
            {
               
                string message = "there are no available rooms for this date";

                TempData["Message"] = message;
                return RedirectToAction("Index", new { id = id });

            }

        }









    }
}
