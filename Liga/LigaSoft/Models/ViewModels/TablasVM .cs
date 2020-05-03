using System.Collections.Generic;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.Models.ViewModels
{
	public class TablasVM
	{
		public TablasVM()
		{
			TablasPorCategoria = new List<TablaCategoriaVM>();
			TablaGeneral = new TablaCategoriaVM();
		}

		public int ZonaId { get; set; }
		public string Titulo { get; set; }
		public bool VerGoles { get; set; }
		public List<TablaCategoriaVM> TablasPorCategoria { get; set; }
		public TablaCategoriaVM TablaGeneral { get; set; }

		public string SeVenLosGolesEnLaTabla()
		{
			return $"Goles a favor en contra y diferencia se ven en la web pública: {VerGoles.ToSiNoString()}";
		}
	}
}