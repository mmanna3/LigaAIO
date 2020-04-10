using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class ConceptoInsumoAgregarStockVM : ConceptoInsumoVM
	{
		[Display(Name = "Stock a sumar")]
		public int StockASumar { get; set; }
	}

}