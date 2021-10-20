using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class InformeCantidadDeJugadoresPorTorneoVM
	{
		public InformeCantidadDeJugadoresPorTorneoVM()
		{
			Renglones = new List<InformeJugadoresPorTorneoRenglonVM>();
		}

		public List<InformeJugadoresPorTorneoRenglonVM> Renglones { get; set; }
	}

	public class InformeJugadoresPorTorneoRenglonVM
	{
		public string TorneoTipo { get; set; }
		public int CantidadDeJugadores { get; set; }
	}
}