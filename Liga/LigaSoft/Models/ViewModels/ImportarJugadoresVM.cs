using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LigaSoft.Models.ViewModels
{
	public class ImportarJugadoresVM
	{		
		public int EquipoEnElQueLoEstoyFichandoId { get; set; }

		[Display(Name = "Elegir archivo .zip")]
		public HttpPostedFileBase JugadoresZip { get; set; }
	}
}