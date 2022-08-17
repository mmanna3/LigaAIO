using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class HabilitacionMasivaVM
	{		
		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public int[] JugadoresSeleccionados { get; set; }
	}
}