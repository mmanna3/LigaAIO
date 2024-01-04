using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class MovimientoSalidaVM : MovimientoVM
	{
		[Display(Name = "Forma de pago")]
		public string FormaDePagoDescripcion { get; set; }
		
		[Display(Name = "Forma de pago")]
		public FormaDePago FormaDePago { get; set; }
	}
}