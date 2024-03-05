using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class EliminacionDirectaConfiguracionVM : ViewModelConId
	{
		public FaseDeEliminacionDirectaEnum? TipoDeLlave { get; set; }
		public string Torneo { get; set; }
		public int TorneoId { get; set; }
		public string TodosLosEquipos { get; set; }
		public int[] EquiposDeLaLlaveResult { get; set; }

		[YKNRequired, Display(Name = "Nombre de la llave")]
		public string LlaveEliminacionDirectaNombre { get; set; }
		

		public EliminacionDirectaConfiguracionVM()
		{ }

		public EliminacionDirectaConfiguracionVM(int torneoId, string torneo, List<IdDescripcionVM> todosLosEquipos)
		{
			TorneoId = torneoId;
			Torneo = torneo; 
			TodosLosEquipos = new JavaScriptSerializer().Serialize(todosLosEquipos);
		}
	}

}