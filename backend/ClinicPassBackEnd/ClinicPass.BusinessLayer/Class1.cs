namespace ClinicPass.BusinessLayer;
using ClinicPass.BusinessLayer.Interfaces;

using System.Collections.Generic;

public class AuthService : IAuthService
{
	public string GenerateJwtToken(int userID, string username, IList<string> roles)
	{
		throw new NotImplementedException();
	}

	public string HashPassword(string plainPassword)
	{
		throw new NotImplementedException();
	}

	public bool VerifyPassword(string plainPassword, string hashedPassword)
	{
		throw new NotImplementedException();
	}
}

