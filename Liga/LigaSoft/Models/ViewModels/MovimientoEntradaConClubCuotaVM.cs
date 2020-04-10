using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class MovimientoEntradaConClubCuotaVM : MovimientoEntradaConClubVM
	{
		[YKNRequired]
		public Mes Mes { get; set; }
	}
}