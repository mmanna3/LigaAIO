using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class DelegadoVM : ViewModelConId
	{
		[YKNRequired, YKNStringLength(Maximo = 30)]
		public string Descripcion { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 20), RegularExpression(@"^[0-9]*$")]
		public string Telefono { get; set; }

		public string Club { get; set; }
		public int ClubId { get; set; }
	}
}