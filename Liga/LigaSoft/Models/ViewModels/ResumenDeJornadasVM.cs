using System.Collections.Generic;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.Models.ViewModels
{
	public class ResumenDeJornadasVM
	{
		public ResumenDeJornadasVM(string titulo, int torneoId, int zonaId)
		{
			JornadasPorFecha = new List<JornadasPorFechaVM>();
			Categorias = new List<CategoriaVM>();
			Titulo = titulo;
			TorneoId = torneoId;
			ZonaId = zonaId;
		}

		public ResumenDeJornadasVM() //Si no meto un constructor por defecto, no me deja postear las jornadas verificadas. Es una cagada, pero no tuve ganas de hacerlo mejor, disculpá.
		{
		}

		public string Titulo { get; set; }
		public List<JornadasPorFechaVM> JornadasPorFecha { get; set; }
		public List<CategoriaVM> Categorias { get; set; }

		//Esto lo voy a postear cuando verifique resultados
		public int[] JornadasVerificadasId { get; set; }
		public int TorneoId { get; set; }
		public int ZonaId { get; set; }
	}

	public class JornadasPorFechaVM
	{
		public JornadasPorFechaVM(int fechaId, int fechaNumero, bool fechaPublicada)
		{
			FechaId = fechaId;
			FechaNumero = fechaNumero;
			Renglones = new List<JornadasPorFechaRenglonVM>();
			Publicada = fechaPublicada.ToSiNoString();
			PublicadaBool = fechaPublicada;
		}		
		public int FechaId { get; set; }
		public int FechaNumero { get; set; }
		public List<JornadasPorFechaRenglonVM> Renglones { get; set; }
		public string Publicada { get; set; }
		public bool PublicadaBool { get; set; }

		public void Ordenar()
		{
			Renglones.Sort((y, x) => x.PuntosTotales.CompareTo(y.PuntosTotales));
		}
	}

	public class JornadasPorFechaRenglonVM
	{
		public JornadasPorFechaRenglonVM()
		{
			ResultadosPorCategorias = new List<ResultadosPorCategoriaVM>();
			PuntosTotales = 0;
			PartidosJugados = 0;
		}

		public int JornadaId { get; set; }
		public int JornadaNumero { get; set; }
		public int EquipoId { get; set; }
		public string Equipo { get; set; }
		public List<ResultadosPorCategoriaVM> ResultadosPorCategorias { get; set; }
		public int PuntosTotales { get; set; }
		public int PartidosJugados { get; set; }
		public string PartidoVerificado { get; set; }
	}

	public class ResultadosPorCategoriaVM
	{
		public int Orden { get; set; }
		public string Goles { get; set; }
	}
}