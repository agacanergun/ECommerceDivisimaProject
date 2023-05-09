using Divisima.BL.Repositories;
using Divisima.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddDbContext<SqlContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
//custom authentication in .net core
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opt.LoginPath = "/admin/Login";
    opt.LogoutPath = "/admin/Logout";
}

);


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/hata/{0}");
    //hata olursa hata sayfasýna yönlendiricez hata sayfasý hazýrlamayý unutma
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); //kimlik doðrulama
app.UseAuthorization(); //kimlik yetkilendirme
app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");


app.Run();
