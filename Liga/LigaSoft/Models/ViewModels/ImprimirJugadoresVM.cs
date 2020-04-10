using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class ImprimirJugadoresVM
	{		
		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public List<int> JugadoresSeleccionados { get; set; }
	}
}