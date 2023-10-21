namespace NewMVCProjekt.Middleware
{
    public class TokenChackMiddleware
    {


      
            private readonly RequestDelegate _next;
            private readonly IAccessTokenService _token;
            

       

            public TokenChackMiddleware (RequestDelegate next, IAccessTokenService token)
            {
                _next = next;
                _token = token;
            }


        public async Task Invoke(HttpContext context)
        {


            string? authorizationHeader = context.Session.GetString("AuthToken");
            var path = context.Request.Path;
           

            if (!string.IsNullOrEmpty(authorizationHeader)&& path!= "/Identity/RefreshToken")
            {
                var ValidToken = _token.AccessTokenValid(authorizationHeader);
                var TokenLiveTime = _token.AccessTokenLiveTime(authorizationHeader);

                if (ValidToken && TokenLiveTime < TimeSpan.FromMinutes(1))
                {


               
                    
                    context.Response.Redirect("/Identity/RefreshToken");
                    
                    return;
                }
                if (!ValidToken)
                {
                   
                   
                    context.Response.Redirect("/Identity/RefreshToken");
                    return ;
                }
            }











            await _next(context);
           
       
                
          }
    }




}

































