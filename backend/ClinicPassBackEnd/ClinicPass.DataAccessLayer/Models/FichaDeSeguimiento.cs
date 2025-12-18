using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicPass.DataAccessLayer.Models
{
    public class FichaDeSeguimiento
    {
        [Key]
        public int IdFichaSeguimiento { get; set; }



        // FK Profesional
        public int IdUsuario { get; set; }
         
        [ForeignKey(nameof(IdUsuario))]
		public Profesional Profesional { get; set; } = null!;



		// FK Historia Clínica
		public int IdHistorialClinico { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; } = null!;

        //FK tratamiento opcional
        public int? TratamientoId { get; set;}
        public Tratamiento? Tratamiento { get; set; }



        //public DateTime FechaPase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }



        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}