using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Enums
{
	public enum EstadoJugadorFichadoPorDelegado
	{
		[Display(Name = "Pendiente de aprobación")]
		PendienteDeAprobacion = 1,
		[Display(Name = "Aprobado")]
		Aprobado = 2,
		[Display(Name = "Rechazado")]
		Rechazado = 3,
	}
}