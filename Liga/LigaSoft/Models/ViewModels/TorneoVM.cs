using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class TorneoVM : ViewModelConId
	{
		[Display(Name = "Año")]
		public Anio Anio { get; set; }

		[Display(Name = "Tipo")]
		public int TipoId { get; set; }

		[Display(Name = "Tipo")]
		public string TipoDesc { get; set; }

		public List<SelectListItem> Tipos { get; set; }

		[Display(Name = "Visible en web pública")]
		public string VisibleEnWebPublica { get; set; }

		[Display(Name = "Zonas")]
		public string Zonas { get; set; }

		[Display(Name = "Categorías")]
		public string Categorias { get; set; }

		[Display(Name = "Formato")]
		public string Formato { get; set; }

		[Display(Name = "Sanciones habilitadas")]
		public string SancionesHabilitadas { get; set; }

		public string Descripcion()
		{
			return $"{TipoDesc} {Anio.Descripcion()}";
		}
	}
}