using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClinicPass.BusinessLayer.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _config; //Configuraciones del servicio en appsettings

		//Constructor
		public AuthService(IConfiguration config)
		{
			_config = config;
		}


		public string HashPassword(string plainPassword) //Convierte contraseña en un Hash y lo devuelve
		{
			using var sha = SHA256.Create();
			var hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(plainPassword)); //hasheo
			return Convert.ToBase64String(hashedPassword);

		}

		public bool VerifyPassword(string plainPassword, string hashedPassword)
		{
			return hashedPassword == HashPassword(plainPassword); //se verifica que la contraseña sea la misma (se hashea la segunda contraseña y se compara)
		}



		public string GenerateJwtToken(int userID, string username, IList<string> roles)
		{
			//Firma y credenciales de seguridad
			var keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
			var key = new SymmetricSecurityKey(keyBytes);
			var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);




			//Claims
			var claims = new List<System.Security.Claims.Claim>
			{
				new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, userID.ToString()),
				new System.Security.Claims.Claim(ClaimTypes.Name, userID.ToString()),

				//Claim Personalizado
				new System.Security.Claims.Claim("username", username),
				new System.Security.Claims.Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
			};





			//Roles/Permisos
			foreach (var role in roles)
			{
				claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, role));
			}

			//Expiración en minutos

			var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpirationMinutes"]));

			//token Descriptor 
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expires,
				SigningCredentials = signinCredentials
			};

			//Creacion y escritura del Token 
			var securityTokenHandler = new JwtSecurityTokenHandler();
			var token = securityTokenHandler.CreateToken(tokenDescriptor);

			//retorno del token
			return securityTokenHandler.WriteToken(token);


			throw new NotImplementedException();
		}




	}
}
