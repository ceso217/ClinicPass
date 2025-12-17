using ClinicPass.DataAccessLayer.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Interfaces
{
	public interface IAuthService
	{
		string HashPassword(string plainPassword); //siempre se le pasa un texto plano y devuelve un hash
		bool VerifyPassword(string plainPassword, string hashedPassword); //se verifica que el hasheo y el password plano sean iguales

		string GenerateJwtToken(int userID, string username, IList<string> roles);

		Task<IdentityResult> ChangePasswordAsync(ChangePasswordDTO dto);

        Task<IdentityResult> AdminResetPasswordAsync(ResetPasswordDTO dto);

    }
}
