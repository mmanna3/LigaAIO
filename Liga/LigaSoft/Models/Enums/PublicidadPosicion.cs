using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum PublicidadPosicion
	{
		[Display(Name = "Izquierda superior")]
		IzquierdaSuperior = 1,
		[Display(Name = "Derecha superior")]
		DerechaSuperior = 2,
		[Display(Name = "Izquierda inferior")]
		IzquierdaInferior = 3,
		[Display(Name = "Derecha inferior")]
		DerechaInferior = 4,
	}
}