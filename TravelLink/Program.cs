var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(

  options =>
  {
      options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
  }


  );


builder.Logging.AddFile(options =>

{   options.RootPath = builder.Environment.ContentRootPath;
    options.BasePath = "Logs";
    options.DateFormat = "yyyyMMdd";
    options.CounterFormat = "000";
    options.FileAccessMode = LogFileAccessMode.KeepOpenAndAutoFlush;
    options.FileEncoding = Encoding.UTF8;
    options.MaxFileSize = 10485760;
    options.IncludeScopes = true;
    options.MaxQueueSize = 100;
    options.Files = new[]
     {

      
        new LogFileOptions
        {
            Path =  "<date:yyyy>/<date:MM>/<counter>.log"
        }
     };



}

);























builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourSessionCookieName";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    
    

});


builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAccessTokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<HotelParceService>();
builder.Services.AddScoped<HotelListingService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Consts.issuer,
            ValidateAudience = true,
            ValidAudience = Consts.audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = Consts.GetSummetricKey()


            

        };
    });
builder.Services.AddAuthorization();
;








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();


app.Use(async (context, next) =>
{
    string? authorizationHeader = context.Session.GetString("AuthToken");
    if (!string.IsNullOrEmpty(authorizationHeader))
    {
        context.Request.Headers.Add("Authorization", authorizationHeader);
        
    }
    
    await next();
});

app.UseMiddleware<TokenChackMiddleware>();
app.UseAuthentication();   

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();


