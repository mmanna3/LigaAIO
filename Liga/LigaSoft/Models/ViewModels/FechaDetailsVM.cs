using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class FechaDetailsVM : ViewModelConId
	{
		[YKNRequired]
		[Display(Name = "Día de la fecha")]
		public string DiaDeLaFecha { get; set; }

		public string Titulo { get; set; }

		[Display(Name = "Jornadas")]
		public IEnumerable<string> Jornadas { get; set; }

		public string Publicada { get; set; }
	}
}