namespace TraveLink.Services
{
    public class HotelListingService
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ApplicationDbContext context;
        public HotelListingService(IHttpContextAccessor contextAccessor, ApplicationDbContext context)
        {




            this.contextAccessor = contextAccessor;
            this.context = context;

        }



        public async Task<int> GetTotalHotels()
        {
            var total = await context.hotels.CountAsync();
            int totalPages = (int)Math.Ceiling((double)total / 20);

            return totalPages;


        }

        public async Task<List<HotelViewModel>> GetHotelViewModels(int page)
        {
            int pageSize = 20;
            var hotels = await context.hotels
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();

            return hotels;





        }





















    }


}
