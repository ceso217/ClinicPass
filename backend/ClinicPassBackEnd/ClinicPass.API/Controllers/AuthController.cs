using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//Directivas y dependencias
using System.Security.Cryptography;
using System.Text;
using ClinicPass.BusinessLayer.Interfaces;

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
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		//Endpoint Login
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO request)
		{
			//verificar credenciales
			if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
			{
				return BadRequest($"No puede haber credenciales vacias: {request.Username} , {request.Password}");

			}


			//validar las credenciales ingresadas, verificando que el Username Existe en la base de datos.
			var user = await _userManager.FindByNameAsync(request.Username);

			//si el usuario no existe devolver un Unauthorized
			if (user == null)
			{
				return Unauthorized();
			}

			//si el usuario existe verificar contraseña, si no es correcta devolver Unauthorized.

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized();
			}


			//Obtener roles del usuario 
			var roles = await _userManager.GetRolesAsync(user);

			//si todo es correcto genero un token JWT
			var token = _authService.GenerateJwtToken(user.Id, user.UserName, roles);

			return Ok(new { user, token });


		}

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

