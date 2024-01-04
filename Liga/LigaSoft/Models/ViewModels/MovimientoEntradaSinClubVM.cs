using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class MovimientoEntradaSinClubVM : MovimientoVM
	{
		[Display(Name = "Forma de pago")]
		public string FormaDePagoDescripcion { get; set; }
		
		[Display(Name = "Forma de pago")]
		public FormaDePago FormaDePago { get; set; }
	}
}