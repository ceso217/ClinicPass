using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
    public class ProfesionalService
    {
        private readonly ClinicPassContext _db;
        private readonly UserManager<Profesional> _userManager;

        public ProfesionalService(ClinicPassContext db, UserManager<Profesional> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

//        private string HashPassword(string password)
//        {
//            using var sha256 = SHA256.Create();
//            var bytes = Encoding.UTF8.GetBytes(password);
//            var hash = sha256.ComputeHash(bytes);
//            return Convert.ToHexString(hash);
//        }

    }
}
