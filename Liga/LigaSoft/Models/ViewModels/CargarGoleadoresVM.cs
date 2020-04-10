namespace LigaSoft.Models.ViewModels
{
	public class CargarGoleadoresVM
	{
		public int JornadaId { get; set; }
		public int PartidoId { get; set; }
		public int CategoriaId { get; set; }
		public string Titulo { get; set; }

		public string EquipoLocalNombre { get; set; }
		public int TotalDeGolesDelLocal { get; set; }

		public string EquipoVisitanteNombre { get; set; }
		public int TotalDeGolesDelVisitante { get; set; }

		public string TodosLosJugadoresDelLocal { get; set; }
		public string TodosLosJugadoresDelVisitante { get; set; }

		public int[] GoleadoresDelLocal { get; set; }
		public int[] CantidadDeGolesGoleadorLocal { get; set; }		

		public int[] GoleadoresDelVisitante { get; set; }
		public int[] CantidadDeGolesGoleadorVisitante { get; set; }
	}
}