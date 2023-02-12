using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LigaSoft.Models.ViewModels
{
	public class ParametroGlobalVM : ViewModelConId
    {
	    [Display(Name = "Escudo por defecto")]
		public string EscudoActual { get; set; }

	    [Display(Name = "Elegir nuevo escudo (Formato JPG y tamaño 100x100px)")]
	    public HttpPostedFileBase EscudoNuevo { get; set; }
	}
}