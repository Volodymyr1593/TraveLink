﻿


namespace TraveLink.Models
{
    public class AppUserViewModel : IdentityUser

    {
        public override string? Id { get; set; }
        [Required(ErrorMessage = "Enter  User Name ")]
        [StringLength(30)]
        public override string? UserName { get; set; }
        [Required(ErrorMessage = "Enter  First Name")]
        [StringLength(30)]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Enter  Last Name")]
        [StringLength(30)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter Email  ")]
        [EmailAddress]
        public  override string? Email { get; set; }

        [StringLength(30)]
        [UIHint("Password")]
        [Required(ErrorMessage = "Enter Password   ")]
        [NotMapped]


        public string? Password{ get; set; }

        [Phone]
        public override string? PhoneNumber { get; set; }
        
        [NotMapped]
         public HotelRoomData? OrederData { get; set; }
        [NotMapped]
         public SearchReserveModel? SearchData { get; set; }
        [NotMapped]
        public virtual ICollection<BookingModel>? Bookings { get; set; }

        [NotMapped]
        [UIHint("Password")]
        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password", ErrorMessage = "passwords do not match")]
        public string? PasswordConfirmd { get; set; }

        public void HashPassword()
        {

            
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);

            
        }
    }
}