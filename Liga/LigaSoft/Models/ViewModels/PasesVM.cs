using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class PasesVM
	{		
		public int EquipoOrigenId { get; set; }

		public string EquipoOrigen { get; set; }

		[YKNRequired]
		public int EquipoDestinoId { get; set; }

		public int[] JugadoresSeleccionados { get; set; }
	}
}