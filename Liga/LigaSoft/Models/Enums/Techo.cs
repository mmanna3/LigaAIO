using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum Techo
	{
		[Display(Name = "No")]
		No,
		[Display(Name = "Sí")]
		Si,
		[Display(Name = "Indeterminado")]
		Indeterminado
	}
}