
using BCrypt.Net;


namespace TraveLink.Controllers
{
    public class Identitycontroller : Controller
    {


        private readonly ApplicationDbContext context;
        private readonly IAccessTokenService token;
        private readonly IRefreshTokenService refreshToken;
        private readonly IUserService userService;

        public Identitycontroller(ApplicationDbContext context, IAccessTokenService token, IRefreshTokenService refreshToken, IUserService userService)
        {
            this.context = context;
            this.token = token;
            this.refreshToken = refreshToken;
            this.userService = userService; 
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
                var userToken = token.GenAccessToken(user);
                var refreshTokenValue = refreshToken.GenRefreshToken(user);

              
                var userRefreshToken = refreshToken.GetUserRefreshToken(refreshTokenValue, user);
                await context.aspnetusertokens.AddAsync(userRefreshToken);
                context.SaveChanges();
                string authorizationHeader = "Bearer " + userToken;
                HttpContext.Session.SetString("AuthToken", authorizationHeader);
                return RedirectToAction("Useracount");
                

            }







            else
            {

                ModelState.AddModelError("Username", "Incorrect User Name or Password");

                return View("index", model);
            }

        }

        
        public async Task<IActionResult> AutenticationAdmin(AppUserViewModel model)

        {


            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AuthToken")))
            {
                return RedirectToAction("Index", "Home");
            }






            if (model.Password == null || model.UserName == null)
            {
                return View(model);

            }



            var user = await context.aspnetusers.FirstOrDefaultAsync(u => u.UserName == model.UserName )??null;
            var roleIds = await context.UserRoles
               .Where(ur => ur.UserId == user.Id)
               .Select(ur => ur.RoleId)
               .ToListAsync()?? null ;
            var roleNames = await context.Roles
           .Where(r => roleIds.Contains(r.Id))
           .Select(r => r.Name)
           .ToListAsync()??null;
            var adminRole = roleNames.FirstOrDefault(r => r == "Admin");
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash) &&  !string.IsNullOrEmpty(adminRole))
            {
                var usertoken = token.GenAccessToken(user, adminRole);
                var refreshTokenValue = refreshToken.GenRefreshToken(user);
                var userRefreshToken = refreshToken.GetUserRefreshToken(refreshTokenValue, user);
               
                await context.aspnetusertokens.AddAsync(userRefreshToken);
                context.SaveChanges();
                string authorizationHeader = "Bearer " + usertoken;
                HttpContext.Session.SetString("AuthToken", authorizationHeader);
                return RedirectToAction("Index","Admin");


            }







            else
            {

                ModelState.AddModelError("Username", "Incorrect User Name or Password");

                return View( model);
            }

        }

        [Authorize]
        public async Task <IActionResult> UserAcount(HotelViewModel? model)

        {
            var userModel = userService.GetUserInform() ?? new AppUserViewModel();
            var searchData = userService.GetSearchInform()??new SearchReserveModel() ;
            userModel.OrederData = model?.reserveData?? new HotelRoomData();
            userModel.SearchData = searchData;
            await userService.SaveOrder(userModel);
            userModel = await userService.GetOrder(userModel);


















            return View(userModel);
        }











    }

}

