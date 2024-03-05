using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Attributes.GPRPattern;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace LigaSoft.Controllers
{
	public class EliminacionDirectaController : Controller
    {
		private readonly ApplicationDbContext Context;
		private readonly EliminacionDirectaVMM VMM;

		public EliminacionDirectaController()
		{
			Context = new ApplicationDbContext();
			VMM = new EliminacionDirectaVMM();
		}

		[ImportModelStateFromTempData]
		public ActionResult Llaves(int id)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == id);

			if (torneo.LlaveDeEliminacionDirecta == null)
				return RedirectToAction("Configurar", new { id });

			var equipos = ObtenerEquiposParaLlaves(id);

			var partidos = Context.PartidosDeEliminacionDirecta.Where(x => x.TorneoId == id).ToList();

			var partidosVM = VMM.MapPartidos(partidos);

			partidosVM = VMM.CompletarCategorias(torneo.Categorias.ToList(), partidosVM);

			partidosVM = VMM.CompletarPartidosDeTodasLasFases((FaseDeEliminacionDirectaEnum)torneo.LlaveDeEliminacionDirecta, partidosVM);

			var vm = new EliminacionDirectaVM(torneo.Id, torneo.Descripcion, (FaseDeEliminacionDirectaEnum)torneo.LlaveDeEliminacionDirecta, partidosVM, equipos);

			return View(vm);
		}

		private List<IdDescripcionVM> ObtenerEquiposParaLlaves(int torneoId)
		{
			var equiposEliminacionDirecta = Context.EquiposEliminacionDirecta.Where(x => x.TorneoId == torneoId).Select(x => x.EquipoId).ToList();

			var equipos = Context.Equipos
				.Where(x => equiposEliminacionDirecta.Contains(x.Id))
				.ToList()
				.Select(x => new IdDescripcionVM { Descripcion = $"{x.Nombre}", Id = x.Id }).ToList();
			if (equipos.Count % 2 != 0)
				equipos.Add(new IdDescripcionVM { Descripcion = $"LIBRE", Id = -1 });

			return equipos;
		}

		[HttpPost, ExportModelStateToTempData]
		public ActionResult Llaves(EliminacionDirectaVM vm)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == vm.TorneoId);

			if (!ModelState.IsValid || HayPartidosSinEquipoOResultado(vm))
				return RedirectToAction("Llaves");

			GuardarLlaves(vm);

			return RedirectToAction("AppInit", "Torneo");
		}

		private bool HayPartidosSinEquipoOResultado(EliminacionDirectaVM vm)
		{
			foreach (var categoria in vm.PartidosPorCategoria)
			{
                foreach (var partido in categoria.PartidosEliminacionDirecta)
                {
                    var noTieneNingunDatoCargado = partido.GolesLocal == null && partido.GolesVisitante == null && partido.LocalId == 0 && partido.VisitanteId == 0;
					var tieneTodosLosDatoCargados = partido.GolesLocal != null && partido.GolesVisitante != null && partido.LocalId != null && partido.VisitanteId != null;
					if (noTieneNingunDatoCargado || tieneTodosLosDatoCargados)
						continue;
					else
					{
						ModelState.AddModelError("", $"Categoría '{categoria.Categoria}': El partido nro {partido.Orden + 1} de la fase '{partido.Fase}' tiene datos incompletos.");
						return true;
					}
                }
            }

			return false;
		}

		private bool HayFasesIncompletas(EliminacionDirectaVM vm)
		{
			foreach (var categoria in vm.PartidosPorCategoria)
			{
				return	ValidarQueLaFaseNoEsteIncompleta(categoria.PartidosEliminacionDirecta.Where(x => x.Fase == FaseDeEliminacionDirectaEnum.Octavos), FaseDeEliminacionDirectaEnum.Octavos) ||
						ValidarQueLaFaseNoEsteIncompleta(categoria.PartidosEliminacionDirecta.Where(x => x.Fase == FaseDeEliminacionDirectaEnum.Cuartos), FaseDeEliminacionDirectaEnum.Cuartos) ||
						ValidarQueLaFaseNoEsteIncompleta(categoria.PartidosEliminacionDirecta.Where(x => x.Fase == FaseDeEliminacionDirectaEnum.Semifinal), FaseDeEliminacionDirectaEnum.Semifinal) ||
						ValidarQueLaFaseNoEsteIncompleta(categoria.PartidosEliminacionDirecta.Where(x => x.Fase == FaseDeEliminacionDirectaEnum.Final), FaseDeEliminacionDirectaEnum.Final);
			}

			return false;
		}

		private bool ValidarQueLaFaseNoEsteIncompleta(IEnumerable<PartidoEliminacionDirectaVM> partidos, FaseDeEliminacionDirectaEnum fase)
		{
			var hayAlMenosUnPartidoConDatosIncompletos = partidos.Any(x => x.GolesLocal == "0" || x.GolesVisitante == "0" || x.LocalId == null || x.VisitanteId == null);
			var hayAlMenosUnPartidoConDatosCompletos = partidos.Any(x => x.GolesLocal != null && x.GolesVisitante != null && x.LocalId != null && x.VisitanteId != null);

			if (hayAlMenosUnPartidoConDatosIncompletos && hayAlMenosUnPartidoConDatosCompletos)
			{
				ModelState.AddModelError("", $"Hay partidos incompletos en la fase {fase}");
				return true;
			}
			return false;
		}

		private void GuardarLlaves(EliminacionDirectaVM vm)
		{
			foreach (var categoria in vm.PartidosPorCategoria)
			{
				foreach (var partidoVM in categoria.PartidosEliminacionDirecta)
				{
					if (partidoVM.GolesLocal == null || partidoVM.GolesVisitante == null || partidoVM.LocalId == null || partidoVM.VisitanteId == null)
						continue;
					
					if (partidoVM.LocalId == -1)
						partidoVM.LocalId = null;
					else if (partidoVM.VisitanteId == -1)
						partidoVM.VisitanteId = null;

					var partidoExistente =
						Context.PartidosDeEliminacionDirecta.SingleOrDefault(x =>
							x.TorneoId == vm.TorneoId &&
							x.CategoriaId == categoria.CategoriaId &&
							x.Orden == partidoVM.Orden &&
							x.Fase == partidoVM.Fase
						);

					if (partidoExistente != null)
					{
						partidoExistente.LocalId = partidoVM.LocalId;
						partidoExistente.VisitanteId = partidoVM.VisitanteId;
						partidoExistente.GolesLocal = partidoVM.GolesLocal;
						partidoExistente.GolesVisitante = partidoVM.GolesVisitante;
						partidoExistente.PenalesLocal = partidoVM.PenalesLocal;
						partidoExistente.PenalesVisitante = partidoVM.PenalesVisitante;
					}
					else
					{
						var nuevoPartido = new PartidoEliminacionDirecta
						{
							TorneoId = vm.TorneoId,
							CategoriaId = categoria.CategoriaId,
							Fase = partidoVM.Fase,
							Orden = partidoVM.Orden,
							LocalId = partidoVM.LocalId,
							VisitanteId = partidoVM.VisitanteId,
							GolesLocal = partidoVM.GolesLocal,
							GolesVisitante = partidoVM.GolesVisitante,
							PenalesLocal = partidoVM.PenalesLocal,
							PenalesVisitante = partidoVM.PenalesVisitante,
						};
						Context.PartidosDeEliminacionDirecta.Add(nuevoPartido);
					}
				}
			}

			Context.SaveChanges();
		}

		[ImportModelStateFromTempData]
		public ActionResult Configurar(int id)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == id);

			var equipos = Context.Equipos.Where(x => x.TorneoId == id).ToList().Where(x => !x.BajaLogica)
			.Select(x => new IdDescripcionVM { Descripcion = $"{x.Id} - {x.Nombre} - Zona: {x.Zona?.Nombre ?? "No tiene" }", Id = x.Id })
			.ToList();

			var configVm = new EliminacionDirectaConfiguracionVM(torneo.Id, torneo.Descripcion, equipos);

			return View("Configurar", configVm);
		}

		[HttpPost, ExportModelStateToTempData]
		public ActionResult Configurar(EliminacionDirectaConfiguracionVM vm)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == vm.TorneoId);
			var equiposDeLaLlave = vm.EquiposDeLaLlaveResult.ToList().Where(x => x != 0);

			if (equiposDeLaLlave.Count() != ((int)vm.TipoDeLlave) && equiposDeLaLlave.Count() != ((int)vm.TipoDeLlave) - 1)
			{
				ModelState.AddModelError("", "La cantidad de equipos no coincide con la fase seleccionada");
				return RedirectToAction("Configurar");
			}

			var hayDuplicados = equiposDeLaLlave.GroupBy(x => x).Any(g => g.Count() > 1);
			if (hayDuplicados)
			{
				ModelState.AddModelError("", "Hay equipos duplicados");
				return RedirectToAction("Configurar");
			}

			torneo.LlaveDeEliminacionDirecta = vm.TipoDeLlave;

			foreach (var equipo in equiposDeLaLlave)
				Context.EquiposEliminacionDirecta.Add(new EquipoEliminacionDirecta { EquipoId = equipo, TorneoId = torneo.Id });

			Context.SaveChanges();

			return RedirectToAction("Llaves", new { id = torneo.Id });
		}

		[HttpPost, ExportModelStateToTempData]
		public ActionResult Eliminar(int torneoId, string palabraDeSeguridad)
		{
			if (palabraDeSeguridad.ToUpper() != "LLAVE")
			{
				ModelState.AddModelError("", "La palabra de seguridad es incorrecta");
				return RedirectToAction("Llaves", new { id = torneoId});
			}
				
			var torneo = Context.Torneos.Find(torneoId);
			torneo.LlaveDeEliminacionDirecta = null;

			var equipos = Context.EquiposEliminacionDirecta.Where(x => x.TorneoId == torneoId);
			Context.EquiposEliminacionDirecta.RemoveRange(equipos);

			var partidos = Context.PartidosDeEliminacionDirecta.Where(x => x.TorneoId == torneoId);
			Context.PartidosDeEliminacionDirecta.RemoveRange(partidos);

			Context.SaveChanges();

			return RedirectToAction("AppInit", "Torneo");
		}

	}
}