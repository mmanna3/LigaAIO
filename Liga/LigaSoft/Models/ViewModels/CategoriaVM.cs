using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class CategoriaVM : ViewModelConId
	{
		[YKNRequired]
		public string Nombre { get; set; }
		public int Orden { get; set; }

		public int TorneoId { get; set; }
		public string Torneo { get; set; }
	}
}