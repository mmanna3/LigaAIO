using System.ComponentModel.DataAnnotations;
using System.Web;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorSancionadoDePorVidaVM : ViewModelConId
    {
		[YKNRequired, YKNStringLength(Maximo = 9), RegularExpression(@"^[0-9]*$", ErrorMessage = "El DNI sólo puede contener números")]
		public string DNI { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Nombre { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Apellido { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 300)]
		public string Motivo { get; set; }
	}
}