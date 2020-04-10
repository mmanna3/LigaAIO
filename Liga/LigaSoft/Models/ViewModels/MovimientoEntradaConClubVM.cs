using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class MovimientoEntradaConClubVM : MovimientoVM
	{
		[YKNRequired, YKNRange(Minimo = 1, ErrorMessage = "Este campo es obligatorio.")]
		public int ConceptoId { get; set; }
		public string Concepto { get; set; }

		[YKNRequired, YKNNumericString, Display(Name = "Precio unit.")]
		public string PrecioUnitario { get; set; }

		[YKNRequired, Display(Name = "Cant")]
		public int Cantidad { get; set; }

		public string Pagado { get; set; }
		public string Deuda { get; set; }

		public int? ClubId { get; set; }
		public string Club { get; set; }

		public string Tipo { get; set; }		
	}
}