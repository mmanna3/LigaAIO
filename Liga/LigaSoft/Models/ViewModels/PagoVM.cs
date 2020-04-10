using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class PagoVM : ViewModelConId
	{
		[YKNRequired, YKNDateTime]
		public string Fecha { get; set; }

		[YKNNumericString]
		public string Importe { get; set; }

		public string Comentario { get; set; }

		[Display(Name = "Creación")]
		public string Alta { get; set; }

		[Display(Name = "Anulacion")]
		public string Anulacion { get; set; }

		public int MovimientoEntradaConClubId { get; set; }

		public string Vigente { get; set; }

		public string TotalDelMovimiento { get; set; }

		public string SaldoDeudor { get; set; }

		public int ClubId { get; set; }
	}

}