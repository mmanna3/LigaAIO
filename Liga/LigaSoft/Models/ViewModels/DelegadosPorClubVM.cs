using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class DelegadosPorClubVM
	{		
		public List<DelegadoPorClubVM> Lista { get; set; }

		public class DelegadoPorClubVM
		{
			public string Nombre { get; set; }

			public string Usuario { get; set; }

			public string Club { get; set; }

			public string Estado { get; set; }			
		}
	}
}