

namespace NewMVCProjekt.Models
{
    public class Login
    {


        [Required(ErrorMessage = "Enter  User Name ")]
         public string username { get; set; }

        [Required(ErrorMessage = "Enter Password   ")]
       public string password { get; set; }
      


    }



}
