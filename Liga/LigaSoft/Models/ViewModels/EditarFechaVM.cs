using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class EditarFechaVM : ViewModelConId
	{
		[Display(Name = "Número")]
		public int Numero { get; set; }

		[YKNRequired]
		[Display(Name = "Día de la fecha")]
		public string DiaDeLaFecha { get; set; }

		public string Titulo { get; set; }

		public string EquiposDeLaZonaJson { get; set; }

		[Display(Name = "Los equipos de la zona son:")]
		public string EquiposDeLaZona { get; set; }

		public int CantidadDeJornadas { get; set; }
		public int[] Locales { get; set; }
		public int[] Visitantes { get; set; }

		public bool HayEquipoLibre { get; set; }

		[YKNRequired]
		public int EquipoLibreId { get; set; }

		public int ZonaId { get; set; }
		
		public List<IdDescripcionVM> LocalesDefault { get; set; }

		public List<IdDescripcionVM> VisitantesDefault { get; set; }

		public IdDescripcionVM EquipoLibreDefault { get; set; }
	}
}