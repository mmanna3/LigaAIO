using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Dominio.Finanzas
{
	public abstract class Concepto
	{
		public int Id { get; set; }

		[Required]
		public string Descripcion { get; set; }		

		public virtual ICollection<MovimientoEntradaConClub> Movimientos { get; set; }

		public abstract string Tipo();
	}

}