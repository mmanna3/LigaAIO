using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
			
			[Display(Name = "Blanqueo pendiente")]
			public string BlanqueoDeClavePendiente { get; set; }
		}
	}
}