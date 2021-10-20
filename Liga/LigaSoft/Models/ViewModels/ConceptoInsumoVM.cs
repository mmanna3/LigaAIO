using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class ConceptoInsumoVM : ViewModelConId
	{
		[YKNRequired]
		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }

		[YKNRequired]
		[Display(Name = "Precio")]
		public int Precio { get; set; }

		[Display(Name = "Stock")]
		public int Stock { get; set; }

		[Display(Name = "Visible")]
		public string Visible { get; set; }
	}

}