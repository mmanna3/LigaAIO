using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class RangoVM
	{		
		[YKNRequired, YKNDateTime, Display(Name= "Fecha de inicio")]
		public string FechaInicio { get; set; }

		[YKNRequired, YKNDateTime, Display(Name = "Fecha de fin")]
		public string FechaFin { get; set; }
	}
}