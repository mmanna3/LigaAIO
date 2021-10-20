using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class PartidoVM : ViewModelConId
	{
		public int JornadaId { get; set; }
		public int FechaId { get; set; }

		public int CategoriaId { get; set; }

		[Display(Name = "Categoría")]
		public string Categoria { get; set; }

		public string Local { get; set; }

		public string Visitante { get; set; }

		[YKNRequired, Display(Name = "Local"), RegularExpression(@"(^[0-9]*$)|(NP)|(np)|(AR)|(ar)|(S)|(s)|(P)|(p)", ErrorMessage = "Sólo números NP, AR, S o P")]		
		public string GolesLocal { get; set; }

		[YKNRequired, Display(Name = "Visitante"), RegularExpression(@"(^[0-9]*$)|(NP)|(np)|(AR)|(ar)|(S)|(s)|(P)|(p)", ErrorMessage = "Sólo números NP, AR, S o P")]
		public string GolesVisitante { get; set; }

		public IEnumerable<string> Goleadores { get; set; }

		public int Orden { get; set; }
	}
}