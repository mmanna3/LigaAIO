using System.ComponentModel.DataAnnotations;
using System.Web;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class PublicidadVM : ViewModelConId
    {
	    [Display(Name = "Título")]
		public string Titulo { get; set; }

	    public string Url { get; set; }

	    [Display(Name = "Posición")]
		public string Posicion { get; set; }

		[Display(Name = "Elegir imagen (Formato JPG)")]
	    public HttpPostedFileBase ImagenNueva { get; set; }
	}
}