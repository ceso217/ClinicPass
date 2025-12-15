using ClinicPass.DataAccessLayer.Data;
using ClinicPass.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
    public class TutorService : ITutorService
    {
        private readonly ClinicPassContext _context;

        public TutorService(ClinicPassContext context)
        {
            _context = context;
        }

        public async Task<Tutor> CrearAsync(Tutor tutor)
        {
            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();
            return tutor;
        }

        public async Task<Tutor?> GetByDniAsync(int dni)
            => await _context.Tutores.FindAsync(dni);
    }
}
