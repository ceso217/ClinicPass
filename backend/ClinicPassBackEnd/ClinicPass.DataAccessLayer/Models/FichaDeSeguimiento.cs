using System.ComponentModel.DataAnnotations;

namespace ClinicPass.DataAccessLayer.Models
{
    public class FichaDeSeguimiento
    {
        [Key]
        public int IdFichaSeguimiento { get; set; }

        /*****************************************************************************************
         * para poder probar turno correctamente(que agrege ficha de seguimiento solo si existe),*
         * tuve que hacer que estos campos puedan ser null para poder crear una ficha de         *
         * seguimiento sin que tire error                                                        *
         * ***************************************************************************************
         */
        //public int IdUsuario { get; set; }
        //public Profesional Profesional { get; set; } = null!;
        public int? UsuarioId { get; set; }
        public Profesional? Profesional { get; set; }

        //public int IdHistorialClinico { get; set; }
        //public HistoriaClinica HistoriaClinica { get; set; } = null!;
        
        public int? HistorialClinicoId { get; set; }
        public HistoriaClinica? HistoriaClinica { get; set; }

        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Observaciones { get; set; }

        public ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public ICollection<PaseDiario> PasesDiarios { get; set; } = new List<PaseDiario>();
    }
}
