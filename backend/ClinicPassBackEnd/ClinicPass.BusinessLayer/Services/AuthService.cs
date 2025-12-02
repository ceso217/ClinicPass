using ClinicPass.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
			//Firma 

			//Claims

			//Roles/Permisos

			//Expiración en minutos

			//Creacion y escritura del Token
			throw new NotImplementedException();
		}

		

		
	}
}
