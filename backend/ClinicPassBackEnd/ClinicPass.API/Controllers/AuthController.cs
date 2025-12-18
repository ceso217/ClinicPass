using ClinicPass.DataAccessLayer.Models;
using ClinicPass.DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//Directivas y dependencias
using System.Security.Cryptography;
using System.Text;
using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClinicPass.DataAccessLayer.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;

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
		public async Task<IActionResult> Register([FromBody] RegisterDTO request)
		{
			if (request.Password != request.RepeatPassword)
			{
				return BadRequest("Las contraseñas no coinciden");
			}

			var exists = await _userManager.Users.AnyAsync(x =>
				x.Dni == request.Dni || x.Email == request.Email);

			if (exists)
			{
				return Conflict("Ya existe un profesional con el DNI o email proporcionado.");
			}

			var user = new Profesional
			{
				UserName = request.Email,
				Email = request.Email,
				NombreCompleto = $"{request.Name} {request.LastName}",
				PhoneNumber = request.PhoneNumber,
				Dni = request.Dni,
				Especialidad = request.Especialidad,
				Activo = request.Activo
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			await _userManager.AddToRoleAsync(user, request.Rol);

			var token = _authService.GenerateJwtToken(
				user.Id,
				user.UserName,
				new[] { request.Rol }
			);

			return Ok(new
			{
				user.Id,
				user.NombreCompleto,
				user.Email,
				user.Dni,
				user.Especialidad,
				user.Activo,
				Token = token
			});
		}

		
		[Authorize]
        [HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO request)
		{
			var result = await _authService.ChangePasswordAsync(request);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new { Errors = errors });
			}
			var successResponse = new SuccessMessageDTO
			{
				Message = $"Contraseña cambiada exitosamente."
			};
			return Ok(successResponse);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
		{
			var result = await _authService.AdminResetPasswordAsync(request);

			if ((!result.Succeeded))
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new { Errors = errors });
			}
			var successResponse = new SuccessMessageDTO
			{
				Message = $"Contraseña reseteada exitosamente."
			};
			return Ok(successResponse);
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

