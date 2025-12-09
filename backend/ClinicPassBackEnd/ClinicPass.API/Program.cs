using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// DbContext (PostgreSQL)
builder.Services.AddDbContext<ClinicPassContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configuración de Identity
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


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();


// Configuración de JWT Authentication
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
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true, // Debe ser True para validar la firma
		IssuerSigningKey = new SymmetricSecurityKey(
			System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
		),
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddAuthorization();

// Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



// Bloque de Seed de roles y usuario Admin
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Profesional>>();

	//  Seed de Roles
	string[] roles = new string[] { "Admin", "Profesional", "Paciente" };
	foreach (var role in roles)
	{
		var roleExists = await roleManager.RoleExistsAsync(role);
		if (!roleExists)
		{
			await roleManager.CreateAsync(new IdentityRole<int>(role));
		}
	}

	// Creación de Usuario Admin por defecto
	var adminName = "admin123";
	var admin = await userManager.FindByNameAsync(adminName);

	if (admin == null)
	{
		var adminUser = new Profesional
		{
			UserName = adminName,
			Email = "admin123@test.com",
			NombreCompleto = "Admin User",
			PhoneNumber = "12345678",
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


// Configuración de Middleware

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🟢 Añadir UseAuthentication y UseAuthorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();