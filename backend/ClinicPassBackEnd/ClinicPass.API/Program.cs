using ClinicPass.BusinessLayer.Services;
using ClinicPass.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Agregar Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro del servicio de Pacientes
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<TratamientoService>(); //registro TratamientoService
builder.Services.AddScoped<HistoriaClinicaService>();//registro HistoriaClinicaService
builder.Services.AddScoped<FichaDeSeguimientoService>();//registro FichaDeSeguimientoService




builder.Services.AddDbContext<ClinicPassContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro Services
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

//Usar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Permitir Autorización si se agrega JWT
app.UseAuthorization();

//Mapear Controladores
app.MapControllers();

app.Run();
