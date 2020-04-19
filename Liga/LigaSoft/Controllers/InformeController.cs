using System;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class InformeController : Controller
    {
	    private readonly ApplicationDbContext _context;
		private readonly InformeVMM _vmm;

		public InformeController()
	    {
		    _context = new ApplicationDbContext();
			_vmm = new InformeVMM(_context);
		}

		[ImportModelStateFromTempData]
		public ActionResult Balance_SeleccionFecha()
	    {
		    return View("Balance_SeleccionFecha");
	    }

		[ExportModelStateToTempData]
	    public ActionResult Balance_Informe(RangoVM vm)
		{
			var fecIni = DateTimeUtils.ConvertToDateTime(vm.FechaInicio);
			var fecFin = DateTimeUtils.ConvertToDateTime(vm.FechaFin);

			if (FechaEsinvalida(fecIni, fecFin))
			    return Balance_SeleccionFecha();

		    var result = _vmm.Map(vm);

			return View(result);
	    }

	    public ActionResult MovimientosImpagos_Informe()
	    {
		    var vm = _vmm.MovimientosImpagosMap();
		    return View(vm);
	    }

	    public ActionResult PagoCuotasPorMes_Informe()
	    {
		    var vm = _vmm.PagoCuotasPorMesMap();
		    return View(vm);
	    }

		private bool FechaEsinvalida(DateTime fechaIni, DateTime fechaFin)
	    {
		    if (fechaIni > fechaFin)
		    {
			    ModelState.AddModelError("", "La fecha de inicio no puede ser mayor a la fecha fin.");
			    return true;
		    }

		    return false;
	    }
	}
}