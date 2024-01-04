using LigaSoft.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("MovimientoEntradaSinClub")]
	public class MovimientoEntradaSinClub : Movimiento
	{
		public FormaDePago FormaDePago { get; set; }
	}

}