using ClinicPass.BusinessLayer.Interfaces;
using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// DbContext - PostgreSQL
// =====================================================
builder.Services.AddDbContext<ClinicPassContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =====================================================
// Identity (Profesional)
// =====================================================
builder.Services.AddIdentity<Profesional, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
})
.AddEntityFrameworkStores<ClinicPassContext>()
.AddDefaultTokenProviders();
// =====================================================
// CORS
// =====================================================


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


// =====================================================
// Servicios (Business Layer)
// =====================================================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IHistoriaClinicaService, HistoriaClinicaService>();
builder.Services.AddScoped<IFichaDeSeguimientoService, FichaDeSeguimientoService>();
builder.Services.AddScoped<ITratamientoService, TratamientoService>();
builder.Services.AddScoped<IHistorialClinicoTratamientoService, HistorialClinicoTratamientoService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<ICoberturaService, CoberturaService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
//IdpacienteIdPaciente  
builder.Services.AddScoped<IProfesionalService, ProfesionalService>();

// (cuando agreguen)
// builder.Services.AddScoped<IProfesionalService, ProfesionalService>();

// =====================================================
// JWT Authentication
// =====================================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        ),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// =====================================================
// Controllers & Swagger
// =====================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =====================================================
// Seed de Roles y Admin
// =====================================================
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Profesional>>();

    string[] roles = { "Admin", "Profesional" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<int>(role));
    }

    var adminUser = await userManager.FindByNameAsync("admin");

    if (adminUser == null)
    {
        var admin = new Profesional
        {
            UserName = "admin",
            Email = "admin@clinicpass.com",
            NombreCompleto = "Administrador",
            Dni = "00000000",
            Activo = true
        };

        var result = await userManager.CreateAsync(admin, "admin123");

        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}

// =====================================================
// Middleware
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowNextApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();