using System;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class PagoController : CommonControllerWithParent<Pago, PagoVM, PagoVMM, MovimientoEntradaConClub, MovimientoEntradaConClubVM, MovimientoEntradaConClubVMM>
    {
	    public PagoController() : base("MovimientoEntradaConClub","MovimientoEntradaConClubId")
	    {			
	    }

	    [HttpPost]
	    public ActionResult Anular(int id)
	    {
		    var model = Context.Pagos.Find(id);

			VMM.MapAnular(model);

			Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	    public ActionResult Auditoria(int id)
	    {
		    var model = Context.Pagos.Find(id);

		    var vm = VMM.MapForDetails(model);

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Create(PagoVM vm)
	    {
		    if (!ModelState.IsValid || ImporteSuperaSaldoDeudor(vm))
			    return RedirectTo("Create", GetParentId(vm));

		    var model = new Pago();

		    VMM.MapForCreate(vm, model);

		    Context.Pagos.Add(model);

		    Context.SaveChanges();

		    return RedirectToAction("Details", "MovimientoEntradaConClub", new {id = vm.MovimientoEntradaConClubId});
	    }

	    private bool ImporteSuperaSaldoDeudor(PagoVM vm)
	    {
		    var mov = Context.MovimientosEntradaConClub.Find(vm.MovimientoEntradaConClubId);

		    if (Convert.ToInt32(vm.Importe) > mov.ImporteAdeudado())
		    {
				ModelState.AddModelError("", "El pago no puede exceder el importe de la deuda.");
			    return true;
		    }
		    return false;
	    }

	    protected override void BeforeReturningCreateView(PagoVM vm)
	    {
		    var movimiento = Context.MovimientosEntradaConClub.Find(vm.MovimientoEntradaConClubId);

		    vm.ClubId = movimiento.ClubId;
			vm.TotalDelMovimiento = $"${movimiento.Total}";
		    vm.SaldoDeudor = $"${movimiento.ImporteAdeudado()}";
	    }
	}
}