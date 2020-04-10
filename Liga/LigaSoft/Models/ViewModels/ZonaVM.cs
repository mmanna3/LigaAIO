using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class ZonaVM : ViewModelConId
	{
		[YKNRequired]
		public string Nombre { get; set; }

		public int TorneoId { get; set; }
		public string Torneo { get; set; }

		[YKNRequired]
		public ZonaTipo Tipo { get; set; }

		[Display(Name = "Tipo")]
		public string TipoDesc { get; set; }

		public List<SelectListItem> TiposDisponibles { get; set; }

		public bool SancionesVisibles { get; set; }
	}
}