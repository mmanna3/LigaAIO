using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum TorneoFormato
	{
		[Display(Name = "Apertura/Clausura")]
		AperturaClausura = 1,
		[Display(Name = "Relámpago")]
		Relampago = 2
	}
}