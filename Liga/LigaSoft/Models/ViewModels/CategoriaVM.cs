using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class CategoriaVM : ViewModelConId
	{
		[YKNRequired]
		public string Nombre { get; set; }
		public int Orden { get; set; }

		public int TorneoId { get; set; }
		public string Torneo { get; set; }
		
		[Display(Name = "Año desde")]
		public int? AnioNacimientoDesde { get; set; }
		
		[Display(Name = "Año hasta")]
		public int? AnioNacimientoHasta { get; set; }
	}
}