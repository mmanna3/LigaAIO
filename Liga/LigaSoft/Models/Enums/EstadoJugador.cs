using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum EstadoJugador
	{
		[Display(Name = "Activo")]
		Activo = 1,
		[Display(Name = "Suspendido")]
		Suspendido = 2,
		[Display(Name = "Inhabilitado")]
		Inhabilitado = 3,
	}
}