using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum ZonaTipo
	{
		[Display(Name = "Apertura")]
		Apertura = 1,
		[Display(Name = "Clausura")]
		Clausura = 2,
		[Display(Name = "Relámpago")]
		Relampago = 3,
		[Display(Name = "Anual")]
		Anual = 4,
	}
}