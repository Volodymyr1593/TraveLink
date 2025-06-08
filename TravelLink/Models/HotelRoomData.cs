namespace TraveLink.Models
{
    [Keyless]
    public class HotelRoomData
    {


        public string? roomtype { get; set; }
        public string? todayprice { get; set; }
        
        public int? hotel_id{ get; set; }
        public List<string>? gests { get; set; }
       
        public List<string>? conditions { get; set; }

        public string[]? selectrooms { get; set; }
        
        public string? hotelname { get; set; } 
        
        public string? hoteladress { get; set; }







       


    }
}
