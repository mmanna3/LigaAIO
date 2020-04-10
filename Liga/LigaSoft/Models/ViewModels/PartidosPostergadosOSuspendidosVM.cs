using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class PartidosPostergadosOSuspendidosVM
	{
		public PartidosPostergadosOSuspendidosVM(string titulo)
		{
			Renglones = new List<RenglonPartidosPostergadosOSuspendidosVM>();
			Titulo = titulo;
		}

		public string Titulo { get; set; }
		public List<RenglonPartidosPostergadosOSuspendidosVM> Renglones { get; set; }		
	}

	public class RenglonPartidosPostergadosOSuspendidosVM
	{
		public string Fecha { get; set; }
		public string Local { get; set; }
		public string Visitante { get; set; }
		public string PostergadoOSuspendido { get; set; }
		public string Categoria { get; set; }
	}
}

