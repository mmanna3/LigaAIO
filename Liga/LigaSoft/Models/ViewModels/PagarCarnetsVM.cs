using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class PagarCarnetsVM
	{
		public int ClubId { get; set; }

		public string Club { get; set; }

		public List<int> CarnetsSeleccionados { get; set; }
	}
}