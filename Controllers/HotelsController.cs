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
            var searchListHotel = HttpContext.Session.GetString("searchListHotel");
           


            if (id != null)
            {


                model = await context.hotels.FindAsync(id);


            }


            if (!string.IsNullOrEmpty(searchListHotel) && id != null)
            {
               var lisHotel = JsonConvert.DeserializeObject<List<HotelViewModel>>(searchListHotel);

                var modelForId = lisHotel.Find(l => l.hotels_id == id);
                if(modelForId != null) {    model = modelForId; }




              
                
                
                
                
                
                
                
            

            }







          

            
          



            if (!string.IsNullOrEmpty(dataListJson))

            {
                



                var dataList = JsonConvert.DeserializeObject<List<HotelRoomData>>(dataListJson);

                if (dataList != null && sesionHotelId == id)
                {


                    model.roomDataModels = dataList;

                }

                   

            }



            return View(model);








        }


        public  async Task <IActionResult> Search(HotelViewModel? model)
        {

            var searchListHotel = HttpContext.Session.GetString("searchListHotel")??"";

            if (!string.IsNullOrEmpty(searchListHotel))
            {

                var searchListHotelJson = JsonConvert.DeserializeObject<List<HotelViewModel>>(searchListHotel);
                return View(searchListHotelJson);

            }


            
           if( !string.IsNullOrEmpty(model?.reserveModel.location))
            {

                var input = model.reserveModel.location.Trim().ToLower();

                var parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                  .Select(p => p.ToLower())
                  .ToList();

                var hotels = context.hotels
                    .AsEnumerable() 
                    .Where(h => parts.Count(p => h.address.ToLower().Contains(p)) >= 2)
                    .ToList();


                var hotelRooms = await hotelParce.MainSearchFilter(hotels, model);
                var hotelRoomsJson = JsonConvert.SerializeObject(hotelRooms);
                HttpContext.Session.SetString("searchListHotel", hotelRoomsJson);


                return View(hotelRooms);

            }

            else
            {
                return View();
            }


  
        
        
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
