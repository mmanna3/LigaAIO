using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class FicharEnOtroEquipoVM
	{		
		public int JugadorId { get; set; }

		public string JugadorNombre { get; set; }

		[YKNRequired, Display(Name = "Equipo")]
		public int NuevoEquipoId { get; set; }		
	}
}