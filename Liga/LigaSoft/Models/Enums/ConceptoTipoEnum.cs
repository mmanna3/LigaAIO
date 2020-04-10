using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum ConceptoTipoEnum
	{
		[Display(Name = "Libre")]
		Libre = 1,
		[Display(Name = "Cuota")]
		Cuota = 2,
		[Display(Name = "Fichaje")]
		Fichaje = 3,
		[Display(Name = "Insumo")]
		Insumo = 4
	}
}