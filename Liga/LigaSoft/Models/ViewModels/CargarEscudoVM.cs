using System.ComponentModel.DataAnnotations;
using System.Web;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class CargarEscudoVM
	{
		public string Titulo { get; set; }
		
		public int ClubId { get; set; }

		[YKNRequired, Display(Name = "Elegir escudo (Formato JPG y tamaño 100x100px)")]
		public HttpPostedFileBase Escudo { get; set; }
	}
}