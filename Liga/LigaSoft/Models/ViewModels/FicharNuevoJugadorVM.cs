namespace LigaSoft.Models.ViewModels
{
	public class FicharNuevoJugadorVM : JugadorBaseVM
	{		
		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public int? IdDelJugadorFichadoAnteriormenteParaImprimir { get; set; }

		public bool HayQueImprimirCarnetDelUltimoJugadorFichado { get; set; }

		public bool ElCarnetEstaPago { get; set; }

		public string LabelGenerarMovimientoFichaje { get; set; }
	}
}