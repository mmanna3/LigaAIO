using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class VersusVM
	{		
		public int FechaId { get; set; }

		public string Descripcion { get; set; }

		public string EquiposDeLaZona { get; set; }

		[Display(Name = "Los equipos de la zona son:")]
		public string EquiposDeLaZonaDescripcion { get; set; }

		public int Cantidad { get; set; }
		public int[] Locales { get; set; }
		public int[] Visitantes { get; set; }

		public bool HayEquipoLibre { get; set; }
		public int EquipoLibreId { get; set; }
		public int ZonaId { get; set; }
	}
}