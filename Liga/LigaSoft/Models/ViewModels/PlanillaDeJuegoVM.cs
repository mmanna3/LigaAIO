using System.Collections.Generic;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class PlanillaDeJuegoVM
	{
		public IList<PlanillaDeJuegoPorCategoriaVM> Planillas { get; set; }
		public string Torneo { get; set; }
		public string Equipo { get; set; }
	}
	
	public class PlanillaDeJuegoPorCategoriaVM
	{
		public string Categoria { get; set; }
		public IList<JugadorParaPlanillaVM> Jugadores { get; set; }
	}
	
	public class JugadorParaPlanillaVM
	{
		public string DNI { get; set; }
		public string Nombre { get; set; }
	}
}