using System.Collections.Generic;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class TorneoController : ABMController<Torneo, TorneoVM, TorneoVMM>
    {
	    public ActionResult AppInit()
	    {
		    return View("Index");
	    }

		[ImportModelStateFromTempData]
	    public override ActionResult Create()
	    {
		    var vm = new TorneoVM{Tipos = Context.TorneoTipos.ToComboValues()};
			return View(vm);
		}

	    [HttpPost]
	    public ActionResult PublicarOcultarTorneo(int id)
	    {
		    var model = Context.Torneos.Find(id);

			if (model != null)
				model.Publico = !model.Publico;

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

	    [HttpPost]
	    public ActionResult PublicarOcultarSanciones(int id)
	    {
		    var model = Context.Torneos.Find(id);

		    if (model != null)
			    model.SancionesHabilitadas = !model.SancionesHabilitadas;

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

		public ActionResult Finalizar(int id)
	    {
		    var model = Context.Torneos.Find(id);

		    var vm = VMM.MapForEditAndDetails(model);

		    return View(vm);
	    }

		[HttpPost]
	    public ActionResult FinalizarPost(int id)
	    {
		    var model = Context.Torneos.Find(id);

		    VMM.MapFinalizar(model);

		    Context.SaveChanges();

		    return View("Index");
	    }
	}
}