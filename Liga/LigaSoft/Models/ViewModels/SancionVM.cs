using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class SancionVM : ViewModelConId
	{
		[YKNRequired, YKNStringLength(Maximo = 300), Display(Name = "Sanción")]
		public string Descripcion { get; set; }

		public int ZonaId { get; set; }
		public int TorneoId { get; set; }

		[Display(Name = "Fecha")]
		public int FechaId { get; set; }
		public List<TextValueItem> FechasDeLaZona { get; set; }

		[Display(Name = "Categoría")]
		public int CategoriaId { get; set; }
		public List<TextValueItem> CategoriasDelTorneo { get; set; }

		[Display(Name = "Jornada")]
		public int JornadaId { get; set; }
		
		[YKNRequired, Display(Name = "Día")]
		public string Dia { get; set; }
		public string Fecha { get; set; }
		public string Local { get; set; }
		public string Visitante { get; set; }
		[Display(Name = "Categoría")]
		public string Categoria { get; set; }

		[Display(Name = "Fechas adeudadas")]
		public int CantidadFechasQueAdeuda { get; set; }

		[Display(Name = "Visible")]
		public string Visible { get; set; }
	}
}