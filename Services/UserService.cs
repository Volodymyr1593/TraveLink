namespace TraveLink.Services
{
    public class UserService:IUserService
    {   
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ApplicationDbContext dbContext;

        public UserService(IHttpContextAccessor contextAccessor, ApplicationDbContext dbContext)
        {
            this.contextAccessor = contextAccessor;
            this.dbContext = dbContext;
        }



        public AppUserViewModel GetUserInform()
        {
            var token = contextAccessor.HttpContext.Session?.GetString("AuthToken");

            const string bearerPrefix = "Bearer ";
            if (token.StartsWith(bearerPrefix))
            {
                // Видалення префікса "Bearer ".
                token = token.Substring(bearerPrefix.Length);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedtoken = tokenHandler.ReadJwtToken(token);
           
            var claims = decodedtoken.Claims.ToList();
            var claimFirst = claims.First();

            var username = claimFirst.Value;
            var claimSecond = claims[1];
            var email = claimSecond.Value;  
            
            var userModel = dbContext?.Users?.FirstOrDefault(u => u.UserName==username && u.Email==email)??new AppUserViewModel();


            return userModel;
        }



        public SearchReserveModel GetSearchInform()

        {
            var modelOrderInform = new SearchReserveModel();
            var ordeInform = contextAccessor.HttpContext.Session?.GetString("CurrentOrderForm")?? " There in no data";
            
            try
            {
                 modelOrderInform = JsonConvert.DeserializeObject<SearchReserveModel>(ordeInform);
              
            }
            catch
            {


                modelOrderInform = new SearchReserveModel();


            }







            return modelOrderInform;
        }




        public async Task  SaveOrder(AppUserViewModel model)



       {

            if ( String.IsNullOrEmpty(model.SearchData.checkindate) != true && model.OrederData != null)
            {


                BookingModel bookingModel = new BookingModel();
                bookingModel.Hotel_Id = model?.OrederData.hotel_id;
                bookingModel.UserId = model?.Id;
                bookingModel.Adult = model?.SearchData?.adult;
                bookingModel.Children = model?.SearchData?.children;
                bookingModel.CheckinDate = model?.SearchData?.checkindate;
                bookingModel.CheckoutDate = model?.SearchData?.checkindate;
                bookingModel.Room = model?.SearchData?.room;
                bookingModel.RoomType = model?.OrederData?.roomtype;
                bookingModel.HotelAdress = model?.OrederData?.hoteladress;
                bookingModel.HotelName = model?.OrederData?.hotelname;
                bookingModel.TodayPrice = model?.OrederData?.todayprice;


                await dbContext.booking.AddAsync(bookingModel);
                await dbContext.SaveChangesAsync();

            }
            else
            {
                return;
            }



        }



        public async Task<AppUserViewModel> GetOrder(AppUserViewModel model)
        {
               var userAndBooking = await dbContext.Users
               .Include(u => u.Bookings)
               .FirstOrDefaultAsync(u => u.Id == model.Id) ?? model;

            return userAndBooking;
        }









    }
}
