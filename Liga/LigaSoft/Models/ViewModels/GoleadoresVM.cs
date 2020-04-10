using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class GoleadoresVM
	{
		public GoleadoresVM(string titulo)
		{
			GoleadoresPorCategoria = new List<GoleadoresCategoriaVM>();
			Titulo = titulo;
		}

		public string Titulo { get; set; }
		public List<GoleadoresCategoriaVM> GoleadoresPorCategoria { get; set; }
	}

	public class GoleadoresCategoriaVM
	{
		public GoleadoresCategoriaVM(string titulo)
		{
			Renglones = new List<RenglonGoleadorVM>();
			Titulo = titulo;
		}

		public string Titulo { get; set; }
		public List<RenglonGoleadorVM> Renglones { get; set; }
	}

	public class RenglonGoleadorVM
	{
		public string Equipo { get; set; }
		public string Jugador { get; set; }
		public int Goles { get; set; }
	}
}

