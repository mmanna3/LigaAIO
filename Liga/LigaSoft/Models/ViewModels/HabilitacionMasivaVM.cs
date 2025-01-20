using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class HabilitacionMasivaVM
	{		
		public int EquipoId { get; set; }

		public EstadoJugador NuevoEstado { get; set; }
		
		public string Equipo { get; set; }

		public int[] JugadoresSeleccionados { get; set; }
	}
}