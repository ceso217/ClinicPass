using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Agregar Controllers
builder.Services.AddControllers();

// 2) Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3) Configurar PostgreSQL
builder.Services.AddDbContext<ClinicPassContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4) Registrar Services
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

// 5) Usar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6) Permitir Autorización si se agrega JWT
app.UseAuthorization();

// 7) Mapear Controladores
app.MapControllers();

app.Run();
