
using BCrypt.Net;





namespace NewMVCProjekt.Controllers
{
    public class Authenticationcontroller : Controller
    {

        
        private readonly ApplicationDbContext context;
        private readonly IAutenticnterface token;

        
       
        public Authenticationcontroller(ApplicationDbContext context, IAutenticnterface token)
        {
           this.context = context;
          this.token = token;

        }


        public IActionResult Index()
        {

            return View();

        }
      




        public IActionResult Registration()
       
        {
            
            return View();

        }


        [HttpPost]
        public IActionResult Registration(AppUserViewModel userViewModel)
        {
            var existingUser =  context.aspnetusers.FirstOrDefault(u => u.UserName == userViewModel.UserName);

            if (existingUser != null)
            {
               
                ModelState.AddModelError("UserName", "Username already taken.");
           
                return View(userViewModel);
            }

            if  (ModelState.IsValid)
            {
                userViewModel.HashPassword();
                context.aspnetusers.AddAsync(userViewModel);
                context.SaveChanges();
                return RedirectToAction("Showuser");

            }
            else
            {
                return View();
            }
        }

       public IActionResult Showuser()
        {
            var Users = context.aspnetusers.OrderBy(p => p.Id).Last();

            return View(Users);

        }

        [HttpPost]
        public IActionResult Autentication(AppUserViewModel model)

        {

            if (model.Password == null || model.UserName == null)
            {
                return View("index", model);

            }



            var user = context.aspnetusers.FirstOrDefault(u => u.UserName == model.UserName);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var Usertoken = token.GetToken(user);


                Response.Headers.Add("Authorization", "Bearer " + Usertoken);


                return RedirectToAction("Useracount");
            }

            else
            {

                ModelState.AddModelError("Username", "Incorrect User Name or Password");

                return View("index", model);
            }

        }


        [Authorize]
       public IActionResult Useracount()

        {
           
            return View();
        }
       






       



    }

  }

