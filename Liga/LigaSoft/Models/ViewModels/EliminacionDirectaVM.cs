using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Script.Serialization;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class EliminacionDirectaVM
	{
		public int TorneoId { get; set; }
		public string Torneo { get; set; }
		public string Equipos { get; set; }
		public string LeyendaEquiposDisponibles { get; set; }

		public FaseDeEliminacionDirectaEnum TipoDeLlave { get; set; }

		public IList<PartidosPorCategoriaVM> PartidosPorCategoria { get; set; }

		public EliminacionDirectaVM() { }

		public EliminacionDirectaVM(int torneoId, string torneo, FaseDeEliminacionDirectaEnum tipoDeLlave, IList<PartidosPorCategoriaVM> partidos, List<IdDescripcionVM> equipos) {
			TorneoId = torneoId;
			Torneo = torneo;
			TipoDeLlave = tipoDeLlave;
			PartidosPorCategoria = partidos;
			Equipos = new JavaScriptSerializer().Serialize(equipos);
			LeyendaEquiposDisponibles = ObtenerLeyendaEquiposDisponibles(equipos);
		}

		public string ObtenerLeyendaEquiposDisponibles(List<IdDescripcionVM> equipos)
		{
			var eq = equipos.Select(x => $"{x.Descripcion}-");
			var union = string.Concat(eq);
			return union.Remove(union.Length - 1);
		}
	}

	public class PartidosPorCategoriaVM
	{
		public int CategoriaId { get; set; }
		public string Categoria { get; set; }
		public IList<PartidoEliminacionDirectaVM> PartidosEliminacionDirecta { get; set; }

		public PartidosPorCategoriaVM() { }

		public PartidosPorCategoriaVM(int categoriaId, string categoria, IList<PartidoEliminacionDirectaVM> partidosEliminacionDirecta)
		{
			CategoriaId = categoriaId;
			Categoria = categoria;
			PartidosEliminacionDirecta = partidosEliminacionDirecta;
		}
	}

	public class PartidoEliminacionDirectaVM
	{
		public FaseDeEliminacionDirectaEnum Fase { get; set; }

		public string Local { get; set; }
		public int LocalId { get; set; }

		public string Visitante { get; set; }
		public int VisitanteId { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesLocal { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesVisitante { get; set; }

		public int? PenalesLocal { get; set; }

		public int? PenalesVisitante { get; set; }

		public PartidoEliminacionDirectaVM()
		{

		}
	}

}