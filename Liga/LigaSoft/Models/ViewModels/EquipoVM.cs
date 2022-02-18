using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class EquipoVM : ViewModelConId
	{			
		[YKNRequired, YKNStringLength(Maximo = 16)]
		public string Nombre { get; set; }

		[YKNRequired, Display(Name = "Club")]
		public int ClubId { get; set; }

		[Display(Name = "Club")]
		public string Club { get; set; }

		[YKNRequired, Display(Name = "Torneo")]
		public int TorneoId { get; set; }

		[Display(Name = "Torneo")]
		public string Torneo { get; set; }

		[Display(Name = "Zona")]
		public string Zona { get; set; }

		[Display(Name = "Código alfanumérico")]
		public string CodigoAlfanumerico { get; set; }

		public List<SelectListItem> TorneosParaCombo { get; set; }

		public List<SelectListItem> ClubsParaCombo { get; set; }

		public List<SelectListItem> Delegados { get; set; }

		[Display(Name = "Jugadores")]
		public IEnumerable<string> Jugadores { get; set; }

		public IEnumerable<int> JugadoresIds { get; set; }

		[Display(Name = "Delegado 1")]
		public int? Delegado1Id { get; set; }

		[Display(Name = "Delegado 2")]
		public int? Delegado2Id { get; set; }

		public string Delegado1 { get; set; }

		public string Delegado2 { get; set; }

		[Display(Name = "Valor de la cuota")]
		public int PrecioDeLaCuota { get; set; }

		[Display(Name = "Cantidad de jugadores fichados")]
		public string CantidadFichados { get; set; }
	}
}