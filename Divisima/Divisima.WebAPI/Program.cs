using Divisima.BL.Repositories;
using Divisima.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SqlContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "izinverilenler", policy => { policy.WithOrigins("http://localhost:5211").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Divisima Web API - Versiyon 1",
        Description = "Bu proje .net core 7.0 ile geliþtirilmiþtir",
        TermsOfService = new Uri("http://www.www.cantacim.com/sozlesme"),
        Contact = new OpenApiContact
        {
            Name = "Aðacan Ergün",
            Email = "agcannn@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
        }
    });
    sw.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5216",
        ValidAudience = "n11",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benimözelkeybilgisi"))
    };
});

var app = builder.Build();
app.UseCors("izinverilenler");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
