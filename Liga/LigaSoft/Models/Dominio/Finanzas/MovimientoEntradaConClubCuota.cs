using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("MovimientoEntradaConClubCuota")]
	public class MovimientoEntradaConClubCuota : MovimientoEntradaConClub
	{
		public Mes Mes { get; set; }
	}
}