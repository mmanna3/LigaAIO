using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class TorneoTipoVM : ViewModelConId
	{
		[YKNRequired]
		[Display(Name = "Descripción")]		
		public string Descripcion { get; set; }

		[YKNRequired]
		[Display(Name = "Validez del carnet (en años)")]		
		public int ValidezDelCarnetEnAnios { get; set; }

		[YKNRequired]
		[Display(Name = "Texto a imprimir en el carnet")]
		public string LoQueSeImprimeEnElCarnet { get; set; }

		public bool Activa { get; set; }

		[YKNRequired]
		[Display(Name = "Formato")]
		public TorneoFormato Formato { get; set; }

		[Display(Name = "Formato")]
		public string FormatoDesc { get; set; }
	}
}