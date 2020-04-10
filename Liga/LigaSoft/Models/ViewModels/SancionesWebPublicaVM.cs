using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class SancionesWebPublicaVM
	{
		public SancionesWebPublicaVM(string titulo)
		{
			Renglones = new List<RenglonSanciones>();
			Titulo = titulo;
		}

		public string Titulo { get; set; }
		public List<RenglonSanciones> Renglones { get; set; }
	}

	public class RenglonSanciones
	{
		public string Dia { get; set; }
		public string Fecha { get; set; }
		public string Local { get; set; }
		public string Visitante { get; set; }
		public string Categoria { get; set; }
		public string Sancion { get; set; }
		public int FechasQueAdeuda { get; set; }
	}
}

