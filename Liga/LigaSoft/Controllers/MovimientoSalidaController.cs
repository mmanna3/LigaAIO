using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class MovimientoSalidaController : CommonController<MovimientoSalida, MovimientoSalidaVM, MovimientoSalidaVMM>
    {
	    [HttpPost]
	    public ActionResult Anular(int id)
	    {
		    var model = Context.MovimientosSalida.Find(id);

			VMM.MapAnular(model);

			Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	    public ActionResult Auditoria(int id)
	    {
		    var model = Context.MovimientosSalida.Find(id);

		    var vm = VMM.MapForDetails(model);

		    return View(vm);
	    }
	}
}