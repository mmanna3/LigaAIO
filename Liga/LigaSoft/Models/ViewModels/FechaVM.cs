using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class FechaVM : ViewModelConId
	{
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

		public int TorneoId { get; set; }
		public int ZonaId { get; set; }

		[Display(Name = "Número")]
		public int Numero { get; set; }


		public void DepurarJornadas()
		{
			var locales = new List<int>();
			var visitantes = new List<int>();

			for (var i = 0; i < Locales.Length; i++)
			{
				if ((Locales[i] != -1 && Locales[i] != -2) || (Visitantes[i] != -1 && Visitantes[i] != -2))
				{
					locales.Add(Locales[i]);
					visitantes.Add(Visitantes[i]);
				}
			}
			Locales = locales.ToArray();
			Visitantes = visitantes.ToArray();
			CantidadDeJornadas = Locales.Length;
		}
	}
}