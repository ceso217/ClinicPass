using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//Directivas y dependencias
using System.Security.Cryptography;
using System.Text;
using ClinicPass.DataAccessLayer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClinicPass.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<Profesional> _userManager;
		private readonly SignInManager<Profesional> _signInManager;
		private readonly IAuthService _authService;

		public AuthController(UserManager<Profesional> userManager, SignInManager<Profesional> signInManager, IAuthService authService)
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<ValuesController>/5
		[HttpPost("register")]
		public string Get(int id)
		{
			return "value";
		}

	}
}
//1.JULIÁN – Autenticación + JWT
//Tareas:
//Crear AuthController
//Crear endpoint /login
//Crear endpoint /register
//Crear DTOs de Login y Registro
//Configurar JWT en Program.cs
//Implementar roles (Admin, Profesional, Recepcionista)
//Proteger endpoints con [Authorize]
//Probar en Swagger usando Bearer Token
//Es un módulo aislado

