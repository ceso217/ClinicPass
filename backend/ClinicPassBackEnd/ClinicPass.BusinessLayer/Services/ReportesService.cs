using ClinicPass.DataAccessLayer.DTOs.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPass.BusinessLayer.Services
{
	public class ReportesService
	{
		public async Task<FiltroFechaDTO> FiltroDeFecha(FiltroFechaDTO? filtro = null)
		{
			DateTime fechaInicio = DateTime.UtcNow.Date;
			DateTime fechaFin = fechaInicio.AddDays(1); // Mañana a las 00:00
			if (filtro.TipoFiltro == FiltroFecha.UltimaSemana)
			{
				fechaInicio = fechaInicio.AddDays(-7);
			}
			else if (filtro.TipoFiltro == FiltroFecha.UltimoMes)
			{
				fechaInicio = fechaInicio.AddDays(-30);
			}
			else if (filtro.TipoFiltro == FiltroFecha.UltimoTrimestre)
			{
				fechaInicio = fechaInicio.AddDays(-90);
			}
			else if (filtro.TipoFiltro == FiltroFecha.UltimoAno)
			{
				fechaInicio = fechaInicio.AddDays(-365);
			}
			else if (filtro.TipoFiltro == FiltroFecha.Personalizado)
			{
				if (filtro == null)
				{
					throw new ArgumentException("Para un filtro personalizado, las fechas son obligatorias.");
				}
				fechaInicio = DateTime.SpecifyKind(filtro.FechaInicio ?? fechaInicio, DateTimeKind.Utc);
				fechaFin = DateTime.SpecifyKind(filtro.FechaFin ?? fechaFin, DateTimeKind.Utc);
			}

			return new FiltroFechaDTO
			{
				FechaInicio = fechaInicio,
				FechaFin = fechaFin
			};
		}
	}
}
