using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class PublicIndexVM
	{
		public IList<AnioWebPublicaVM> Anios { get; set; }		
		
		public DatosDeEquiposVM DatosDeEquipos { get; set; }
		public SancionesWebPublicaVM Sanciones { get; set; }
		public TablasVM Tablas { get; set; }
		public ResumenDeJornadasVM Jornadas { get; set; }
		public FixtureVM Fixture { get; set; }
		public GoleadoresVM Goleadores { get; set; }
		public PublicidadesVM Publicidades { get; set; }		

		public IList<NoticiaVM> Noticias { get; set; }

		public string TorneoSeleccionadoId { get; set; }
		public string AperturaClausuraSeleccionadoId { get; set; }
		public string ZonaSeleccionadaId { get; set; }
		public string AnioSeleccionadoId { get; set; }

		public PublicIndexVM()
		{
			Anios = new List<AnioWebPublicaVM>();
			Noticias = new List<NoticiaVM>();
		}
	}
}