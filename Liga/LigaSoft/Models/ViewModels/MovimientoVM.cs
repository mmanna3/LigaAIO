using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public abstract class MovimientoVM : ViewModelConId
	{
		[YKNRequired, YKNDateTime]
		public string Fecha { get; set; }

		[YKNNumericString]
		public string Total { get; set; }

		public string Comentario { get; set; }

		[Display(Name = "Creación")]
		public string Alta { get; set; }

		[Display(Name = "Anulacion")]
		public string Anulacion { get; set; }

		public string Vigente { get; set; }
	}

}