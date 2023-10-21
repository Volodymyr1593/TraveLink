
using BCrypt.Net;


namespace NewMVCProjekt.Controllers
{
    public class Identitycontroller : Controller
    {


        private readonly ApplicationDbContext context;
        private readonly IAccessTokenService token;
        private readonly IRefreshTokenService refreshToken;

        public Identitycontroller(ApplicationDbContext context, IAccessTokenService token, IRefreshTokenService refreshToken)
        {
            this.context = context;
            this.token = token;
            this.refreshToken = refreshToken;
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
        public async Task<IActionResult> Registration(AppUserViewModel userViewModel)
        {
            var existingUser = await context.aspnetusers.FirstOrDefaultAsync(u => u.UserName == userViewModel.UserName);

            if (existingUser != null)
            {

                ModelState.AddModelError("UserName", "Username already taken.");

                return View(userViewModel);
            }

            if (ModelState.IsValid)
            {
                userViewModel.HashPassword();
                await context.aspnetusers.AddAsync(userViewModel);
                context.SaveChanges();
                return RedirectToAction("Showuser");

            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> LogOut()

        {

            var Accesstoken = HttpContext.Session.GetString("AuthToken");
            var currentRefreshToken = refreshToken.GetRefreshToken(Accesstoken);
             refreshToken.DeleteRefreshToken(currentRefreshToken);
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");



        }

        public async Task<IActionResult> RefreshToken()
        {
            string? referrer = Request.Headers["Referer"].ToString();


           
            string authorizationHeader = Request.Headers["Authorization"];
            bool isValidToken = token.AccessTokenValid(authorizationHeader);
            var refreshtoken = refreshToken.GetRefreshToken(authorizationHeader);


            if (!string.IsNullOrEmpty(refreshtoken) && refreshToken.RefreshTokenValid(refreshtoken)&&isValidToken)

            {
               
                await refreshToken.RefreshAccessToken(authorizationHeader);
                return Redirect(referrer);

               
            }


            else
            {
                refreshToken.DeleteRefreshToken(refreshtoken);
                string message = "SessionExpiredMessage. Your session has expired. Please log in again.";

                TempData["Message"] = message;

              
               
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");

            }









        }



        [HttpPost]
         public async Task   <IActionResult> Autentication(AppUserViewModel model)

        {

             
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AuthToken")))
            {
                return RedirectToAction("Index","Home");
            }
            





            if (model.Password == null || model.UserName == null)
            {
                return View("index", model);

            }



            var user = await  context.aspnetusers.FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var Usertoken = token.GenAccessToken(user);
                var RefreshToken = refreshToken.GenRefreshToken(user);
                 
                UserRefreshToken UserRefreshToken = new UserRefreshToken
                 {
                    UserId = user.Id,
                    LoginProvider = "local", 
                    Name = "refresh_token",
                    Value = RefreshToken
                };
                await context.aspnetusertokens.AddAsync(UserRefreshToken);
                context.SaveChanges();
                string authorizationHeader = "Bearer " + Usertoken;
                HttpContext.Session.SetString("AuthToken", authorizationHeader);
                return RedirectToAction("Useracount");
                

            }







            else
            {

                ModelState.AddModelError("Username", "Incorrect User Name or Password");

                return View("index", model);
            }

        }


        [Authorize]
        public IActionResult UserAcount()

        {
            var Users = context.aspnetusers.OrderBy(p => p.Id).Last();

            return View(Users);
        }











    }

}

