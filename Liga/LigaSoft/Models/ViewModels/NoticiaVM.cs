using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class NoticiaVM : ViewModelConId
	{
		public string Fecha { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 60)]
		public string Titulo { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 140), Display(Name = "Subtítulo")]
		public string Subtitulo { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 4000)]
		public string Cuerpo { get; set; }

		public string Visible { get; set; }

		public string TituloConFecha()
		{
			return $"{Titulo} - {Fecha}";
		}
	}
}