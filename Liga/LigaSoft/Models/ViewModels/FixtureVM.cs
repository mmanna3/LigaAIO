using System.Collections.Generic;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.Models.ViewModels
{
	public class FixtureVM
	{
		public FixtureVM(string titulo)
		{
			Fechas = new List<FixtureFechaVM>();
			Titulo = titulo;
		}

		public int ZonaId { get; set; }
		public int TorneoId { get; set; }
		public string Titulo { get; set; }
		public List<FixtureFechaVM> Fechas { get; set; }
		public bool PublicadoBool { get; set; }		

		public string Publicado()
		{			
			return $"Publicado: {PublicadoBool.ToSiNoString()}";
		}
	}

	public class FixtureFechaVM
	{
		public FixtureFechaVM()
		{
			LocalVisitante = new List<LocalVisitanteVM>();
		}

		public string Titulo { get; set; }
		public string DiaDeLaFecha { get; set; }
		public List<LocalVisitanteVM> LocalVisitante { get; set; }
		public string EquipoLibre { get; set; }
	}
}

