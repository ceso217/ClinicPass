using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ClinicPass.BusinessLayer.Services;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using ClinicPass.DataAccessLayer.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClinicPassContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//Se añade el Identity
builder.Services.AddIdentity<Profesional, IdentityRole<int>>(options =>
{
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
})
    .AddEntityFrameworkStores<ClinicPassContext>()
    .AddDefaultTokenProviders();







builder.Services.AddScoped<ITurnoService, TurnoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


