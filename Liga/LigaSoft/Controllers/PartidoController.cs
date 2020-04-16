using System;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class PartidoController : ABMControllerWithParent<Partido, PartidoVM, PartidoVMM, Jornada, JornadaVM, JornadaVMM>
	{

		public PartidoController() : base("Jornada", "JornadaId")
		{
		}

		[ImportModelStateFromTempData]
		public ActionResult CargarGoleadores(int id) //Estaría bueno que el parámetro se llame partidoId
		{
			var partido = Context.Partidos.Single(x => x.Id == id);
			var goleadores = Context.Goleadores.Where(x => x.PartidoId == id).ToList();
			var vm = VMM.MapForCargarGoleadores(goleadores, partido);
			return View(vm);
		}

		[ExportModelStateToTempData, HttpPost]
		public ActionResult CargarGoleadores(CargarGoleadoresVM vm)
		{			
			ValidarGoleadores(vm);
			if (!ModelState.IsValid)
				return RedirectToAction("CargarGoleadores", new { id = vm.PartidoId });

			var model = Context.Partidos.Find(vm.PartidoId);

			var goleadores = Context.Goleadores.Where(x => x.PartidoId == model.Id);
			Context.Goleadores.RemoveRange(goleadores);

			VMM.MapForCargarGoleadores(vm, model);

			Context.SaveChanges();

			return RedirectToAction("Details", new {id = model.Id});
		}

		private void ValidarGoleadores(CargarGoleadoresVM vm)
		{
			if (vm.GoleadoresDelLocal != null && vm.CantidadDeGolesGoleadorLocal.Any(x => x < 1) 
				|| vm.GoleadoresDelVisitante != null && vm.CantidadDeGolesGoleadorVisitante.Any(x => x < 1))
				ModelState.AddModelError("", "La cantidad de goles no puede ser menor a uno.");

			//if (vm.GoleadoresDelLocal != null && vm.GoleadoresDelLocal.GroupBy(x => x).Any(y => y.Count() > 1) 
			//	|| vm.GoleadoresDelVisitante != null && vm.CantidadDeGolesGoleadorVisitante.GroupBy(x => x).Any(y => y.Count() > 1))
			//	ModelState.AddModelError("", "No puede haber jugadores repetidos.");
		}
	}
}