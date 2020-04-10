using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LigaSoft.Models.ViewModels
{
	public class ModificarEquiposVM : ViewModelConId
	{
		public int ZonaId { get; set; }
		public string Zona { get; set; }
		public string Torneo { get; set; }
		public int TorneoId { get; set; }

		[Display(Name = "Equipos del torneo que no se encuentran en ninguna zona")]
		public int EquipoAAgregarALaZona { get; set; }

		public List<TextValueItem> EquiposDelTorneoSinZonaInicial { get; set; }
		public string EquiposDeLaZonaJson { get; set; }
		public int[] EquiposDelTorneoSinZonaResult { get; set; }
		public int[] EquiposDeLaZonaResult { get; set; }

		public ModificarEquiposVM()
		{ }

		public ModificarEquiposVM(int zonaId, string zona, int torneoId, string torneo, List<SelectListItem> equiposDeLaZona, List<TextValueItem> equiposDeLTorneoSinZona)
		{
			ZonaId = zonaId;
			Zona = zona;
			TorneoId = torneoId;
			Torneo = torneo;

			var equiposDeLaZonaInicial =  new List<SelectListItem>();
			if (equiposDeLaZona != null)
				equiposDeLaZonaInicial.AddRange(equiposDeLaZona);
			EquiposDeLaZonaJson = JsonConvert.SerializeObject(equiposDeLaZonaInicial);

			EquiposDelTorneoSinZonaInicial = new List<TextValueItem>();
			if (equiposDeLTorneoSinZona != null)
				EquiposDelTorneoSinZonaInicial = new List<TextValueItem>(equiposDeLTorneoSinZona);			
		}
	}
}