using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum FormaDePago
	{
		[Display(Name = "Efectivo")]
		Efectivo = 1,
		[Display(Name = "Virtual")]
		Virtual = 2
	}
}