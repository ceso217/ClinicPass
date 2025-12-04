using ClinicPass.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using ClinicPass.BusinessLayer.Services;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;


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







builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Authentication
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey
            (
               System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();





var app = builder.Build();

//seed de roles y admin User
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Profesional>>();

    string[] roles = new string[] { "Admin", "Profesional", "Paciente" };
    foreach (var role in roles)
    {
        var roleExists = await roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
    // crear un usuario Admin por defecto

    var adminName = "admin123";
    var admin = await userManager.FindByNameAsync(adminName);

    if (admin == null)
    {
        var adminUser = new Profesional
        {
            UserName = adminName,
            Email = "admin123@test.com",
            NombreCompleto = "Admin User",
            Telefono = "12345678",
            Activo = true,
            Dni = "12345678"
        };
        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}






if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


