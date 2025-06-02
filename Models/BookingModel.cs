namespace TraveLink.Models
{
    public class BookingModel
    {



       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("hotel_id")]
        public int? Hotel_Id{ get; set; }
        [Column("user_id")]
        public string? UserId { get; set; }
        [Column("hotelname")]
        public string? HotelName { get; set; }
        [Column("hoteladress")]
        public string? HotelAdress { get; set; }
        [Column("checkindate")]
        public string?  CheckinDate { get; set; }
        [Column("checkoutdate")]
        public string? CheckoutDate { get; set; }
        [Column("adult")]
        public string? Adult { get; set; }
        [Column("children")]
        public string? Children { get; set; }
        [Column("room")]
        public string? Room { get; set; }
        [Column("roomtype")]

        public string? RoomType { get; set; }
        [Column("todayprice")]
        public string? TodayPrice { get; set; }




           














    }
}
