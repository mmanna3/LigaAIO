using System.Collections.Generic;
using System.Web.Mvc;

namespace LigaSoft.Models.ViewModels
{
	public class DeshabilitarJugadoresPorTorneoVM
	{
		public List<TextValueItem> TorneoTipos { get; set; }
		
		public int TorneoTipoId { get; set; }
	}
}