using LigaSoft.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("MovimientoSalida")]
	public class MovimientoSalida : Movimiento
	{
		[Required]
		public FormaDePago FormaDePago { get; set; }
	}

}