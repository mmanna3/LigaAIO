using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.Script.Serialization;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class EliminacionDirectaVM
	{
		public int TorneoId { get; set; }
		public string Torneo { get; set; }
		public string Equipos { get; set; }
		public IList<EquiposPorTorneoVM> EquiposPorTorneo { get; set; }
		public FaseDeEliminacionDirectaEnum TipoDeLlave { get; set; }

		public string LlaveEliminacionDirectaPublicada;

		public IList<PartidosPorCategoriaVM> PartidosPorCategoria { get; set; }

		public EliminacionDirectaVM() { }

		public EliminacionDirectaVM(int torneoId, string torneo, FaseDeEliminacionDirectaEnum tipoDeLlave, bool llaveEliminacionDirectaPublicada, IList<PartidosPorCategoriaVM> partidos, List<IdDescripcionVM> equipos, List<EquiposPorTorneoVM> equiposPorTorneo) {
			TorneoId = torneoId;
			Torneo = torneo;
			TipoDeLlave = tipoDeLlave;
			LlaveEliminacionDirectaPublicada = llaveEliminacionDirectaPublicada.ToSiNoString();
			PartidosPorCategoria = partidos;
			Equipos = new JavaScriptSerializer().Serialize(equipos);
			EquiposPorTorneo = equiposPorTorneo;
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
		public int CategoriaOrden { get; set; }

		public IList<PartidoEliminacionDirectaVM> PartidosEliminacionDirecta { get; set; }

		public PartidosPorCategoriaVM() { }

		public PartidosPorCategoriaVM(int categoriaId, string categoria, int orden, IList<PartidoEliminacionDirectaVM> partidosEliminacionDirecta)
		{
			CategoriaId = categoriaId;
			Categoria = categoria;
			CategoriaOrden = orden;
			PartidosEliminacionDirecta = partidosEliminacionDirecta.OrderByDescending(x => x.Fase).ThenBy(x => x.Orden).ToList();
		}
	}

	public class PartidoEliminacionDirectaVM
	{
		public FaseDeEliminacionDirectaEnum Fase { get; set; }
		public int Orden { get; set; }
		
		public string LocalEscudoPath { get; set; }
		public string Local { get; set; }
		public int?	 LocalId { get; set; }

		public string VisitanteEscudoPath { get; set; }
		public string Visitante { get; set; }
		public int? VisitanteId { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)", ErrorMessage = "Formato incorrecto")]
		public string GolesLocal { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)", ErrorMessage = "Formato incorrecto")]
		public string GolesVisitante { get; set; }

		public int? PenalesLocal { get; set; }

		public int? PenalesVisitante { get; set; }

		public PartidoEliminacionDirectaVM()
		{

		}
	}

	public class EquiposPorTorneoVM
	{
		public EquiposPorTorneoVM()
		{
		}

		public IList<string> Equipos { get; set; }
		public string Torneo { get; set; }
	}

}