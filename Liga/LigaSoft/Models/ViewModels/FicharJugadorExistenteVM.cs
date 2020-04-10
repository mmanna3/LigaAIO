using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class FicharJugadorExistenteVM
	{
		[YKNRequired, Display(Name = "Jugador")]
		public int JugadorId { get; set; }
		
		public int EquipoEnElQueLoEstoyFichandoId { get; set; }

		public string EquipoEnElQueLoEstoyFichandoNombre { get; set; }
	}
}