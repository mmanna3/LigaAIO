﻿using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Attributes.GPRPattern;

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

		public ActionResult Llaves(int id)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == id);

			if (torneo.LlaveDeEliminacionDirecta == null)
				return RedirectToAction("Configurar", new { id });
			
			var equipos = Context.EquiposEliminacionDirecta.Where(x => x.TorneoId == id).ToList();

			var partidos = Context.PartidosDeEliminacionDirecta.Where(x => x.TorneoId == id).ToList();

			// Quizás acá podría completar los partidos que no estén, no?
			// Cosa de que le llegue equipo null y resultado 0 (o resultado null), no sé pa mañna
			// Capaz es mejor primero ARRANCAR CON LA VISTA (a ver qué datos le vienen mejor)
			// Porque si puede postear sin problemas este mismo VM sería un tremendísimo fiestón
			// Pero claramente tengo mis dudas
			// En teoría, dice que se puede
			// https://stackoverflow.com/questions/68388855/binding-a-nested-object-in-asp-net-mvc-razor
			var partidosVM = VMM.MapPartidos(partidos);

			partidosVM = VMM.CompletarCategorias(torneo.Categorias.ToList(), partidosVM);

			partidosVM = VMM.CompletarPartidosDeTodasLasFases((FaseDeEliminacionDirectaEnum)torneo.LlaveDeEliminacionDirecta, partidosVM);

			var vm = new EliminacionDirectaVM(torneo.Id, torneo.Descripcion, (FaseDeEliminacionDirectaEnum)torneo.LlaveDeEliminacionDirecta, partidosVM);

			return View(vm);
		}

		[HttpPost]
		public ActionResult Llaves(EliminacionDirectaVM vm)
		{
			var torneo = Context.Torneos.SingleOrDefault(x => x.Id == vm.TorneoId);


			return View();
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


	}
}