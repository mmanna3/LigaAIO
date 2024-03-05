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
			VMM = new EliminacionDirectaVMM(Context);
		}

		[ImportModelStateFromTempData]
		public ActionResult Llaves(int id)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == id);

			if (torneo.LlaveDeEliminacionDirecta == null)
				return RedirectToAction("Configurar", new { id });

			var equipos = ObtenerEquiposParaLlaves();
			var equiposPorTorneo = ObtenerEquiposAgrupadosPorTorneo();
			var partidosVM = VMM.ObtenerPartidosVMParaLlaves(torneo);

			var vm = new EliminacionDirectaVM(torneo.Id, torneo.Descripcion, (FaseDeEliminacionDirectaEnum)torneo.LlaveDeEliminacionDirecta, torneo.LlaveEliminacionDirectaPublicada, partidosVM, equipos, equiposPorTorneo);

			return View(vm);
		}

		private List<EquiposPorTorneoVM> ObtenerEquiposAgrupadosPorTorneo()
		{
			var equipos = Context.Equipos
				.Where(x => !x.BajaLogica)
				.GroupBy(x => x.TorneoId);

			var result = new List<EquiposPorTorneoVM>();

			var torneos = Context.Torneos.Include(x => x.Tipo).ToList();

			foreach (var group in equipos)
			{
				var torneoId = group.Key;
				var torneo = torneos.SingleOrDefault(x => x.Id == torneoId);
				string torneoDescripcion = "Sin torneo";
				if (torneo != null)
					torneoDescripcion = torneo.Descripcion;

				var equiposPorTorneo = new EquiposPorTorneoVM
				{
					Torneo = torneoDescripcion,
					Equipos = group.Select(x => $"{x.Id} - {x.Nombre}").ToList()
				};
				result.Add(equiposPorTorneo);
			}
				
			return result;
		}

		private List<IdDescripcionVM> ObtenerEquiposParaLlaves()
		{
			var equipos = Context.Equipos
				.Where(x => !x.BajaLogica)
				.ToList()
				.Select(x => new IdDescripcionVM { Descripcion = $"{x.Id} - {x.Nombre}", Id = x.Id }).ToList();

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
			if (!ModelState.IsValid)
				return RedirectToAction("Configurar");

			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == vm.TorneoId);			
			torneo.LlaveDeEliminacionDirecta = vm.TipoDeLlave;
			torneo.LlaveEliminacionDirectaNombre = vm.LlaveEliminacionDirectaNombre;

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

		[HttpPost]
	    public ActionResult MostrarOcultarEnWebPublica(int id)
	    {
		    var torneo = Context.Torneos.Find(id);

		    torneo.LlaveEliminacionDirectaPublicada = !torneo.LlaveEliminacionDirectaPublicada;

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	}
}