using System.Collections.Generic;
using System.Web.Mvc;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class SeleccionarEquipoVM
	{
		[YKNRequired]
		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public List<SelectListItem> EquiposParaCombo { get; set; }
	}
}