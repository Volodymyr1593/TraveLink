namespace NewMVCProjekt.Servises
{
    public class SessionCleanupService:BackgroundService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
   
        private readonly BackgroundService _data;

        private readonly IAccessTokenService token;



        public SessionCleanupService(  IHttpContextAccessor httpContextAccessor, BackgroundService data, IAccessTokenService token)
        {
           
            this.token = token;
            this.httpContextAccessor = httpContextAccessor;
            _data = data;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Очікування 1 хвилину

                ClearExpiredSessions();
            }
        }

        public void ClearExpiredSessions()
        {



           var session = httpContextAccessor.HttpContext?.Session;
            var CurrentToken = httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            var ValidToken = token.AccessTokenValid(CurrentToken);

               if (session == null)
                  return;

                var lastAccessedTime = session.GetString("LastAccessedTime");
                var idleTimeout = TimeSpan.FromMinutes(30);

               if (DateTime.TryParse(lastAccessedTime, out var lastAccessed))
                {
                    if (DateTime.UtcNow - lastAccessed > idleTimeout|| ValidToken)
                   {


                        session.SetString("SessionExpiredMessage", "Your session has expired. Please log in again.");
                        session.Clear();
                    }
                }
            
        }

       /// builder.Services.AddHostedService<SessionCleanupService>()



    }
}
