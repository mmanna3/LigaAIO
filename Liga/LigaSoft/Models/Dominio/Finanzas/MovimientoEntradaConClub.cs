using LigaSoft.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("MovimientoEntradaConClub")]
	public class MovimientoEntradaConClub : Movimiento
	{
		[Required]
		public int ConceptoId { get; set; }
		public virtual Concepto Concepto { get; set; }

		public int PrecioUnitario { get; set; }

		public int Cantidad { get; set; }

		public virtual ICollection<Pago> Pagos { get; set; }

		public int ClubId { get; set; }
		public virtual Club Club { get; set; }

		public int ImportePagado()
		{
			return Pagos.Where(x => x.Vigente).Sum(x => x.Importe);
		}

		public int ImporteAdeudado()
		{
			if (Vigente)
				return Total - Pagos.Where(x => x.Vigente).Sum(x => x.Importe);
			return 0;
		}
	}

}