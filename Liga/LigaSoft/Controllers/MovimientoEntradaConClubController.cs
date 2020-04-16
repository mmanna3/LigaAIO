using System;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class MovimientoEntradaConClubController : ABMControllerWithParent<MovimientoEntradaConClub, MovimientoEntradaConClubVM, MovimientoEntradaConClubVMM, Club, ClubVM, ClubVMM>
    {
	    public MovimientoEntradaConClubController() : base("Club","ClubId")
	    {			
	    }

	    [ImportModelStateFromTempData]
		public ActionResult CreateMovimientoInsumo(int clubId)
	    {
		    var club = Context.Clubs.Find(clubId).Nombre;
			var vm = new MovimientoEntradaConClubVM {ClubId = clubId, Club = club, Tipo = "Insumo"};

			return View(vm);
	    }

	    [ImportModelStateFromTempData]
		public ActionResult CreateMovimientoCuota(int clubId)
	    {
		    var club = Context.Clubs.Find(clubId);
			var vm = new MovimientoEntradaConClubCuotaVM { ClubId = clubId, Club = club.Nombre, Tipo = "Cuota", PrecioUnitario = club.Cuota().ToString()};

			return View(vm);
	    }

	    [ImportModelStateFromTempData]
		public ActionResult CreateMovimientoFichaje(int clubId)
	    {
		    var club = Context.Clubs.Find(clubId).Nombre;
		    var vm = new MovimientoEntradaConClubVM { ClubId = clubId, Club = club, Tipo = "Fichaje", Cantidad = 1};

		    return View(vm);
	    }

	    [ImportModelStateFromTempData]
		public ActionResult CreateMovimientoLibre(int clubId)
	    {
		    var club = Context.Clubs.Find(clubId).Nombre;
		    var vm = new MovimientoEntradaConClubVM { ClubId = clubId, Club = club, Tipo = "Libre" };

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Create(MovimientoEntradaConClubVM conClubVM)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction($"CreateMovimiento{conClubVM.Tipo}", new {clubId = conClubVM.ClubId });

		    var model = new MovimientoEntradaConClub();

		    VMM.MapForCreate(conClubVM, model);

		    Context.MovimientosEntradaConClub.Add(model);

		    Context.SaveChanges();

		    var movimientoCreado = Context.MovimientosEntradaConClub.Local.First();
		    return RedirectToAction("Create","Pago", new { parentId = movimientoCreado.Id});
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult CreateMovimientoCuota(MovimientoEntradaConClubCuotaVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("CreateMovimientoCuota", new { clubId = vm.ClubId });

		    var model = new MovimientoEntradaConClubCuota();

		    VMM.MapForCreateMovimientoCuota(vm, model);

		    Context.MovimientosEntradaConClubCuota.Add(model);

		    Context.SaveChanges();

		    var movimientoCreado = Context.MovimientosEntradaConClubCuota.Local.First();
		    return RedirectToAction("Create", "Pago", new { parentId = movimientoCreado.Id });
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult CreateMovimientoInsumo(MovimientoEntradaConClubVM conClubVM)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("CreateMovimientoInsumo", new { clubId = conClubVM.ClubId });

		    var model = new MovimientoEntradaConClub();
		    VMM.MapForCreate(conClubVM, model);
		    Context.MovimientosEntradaConClub.Add(model);

		    var conceptoInsumo = Context.ConceptosInsumo.Single(x => x.Id == conClubVM.ConceptoId);
		    conceptoInsumo.DecrementarStock(conClubVM.Cantidad);

		    Context.SaveChanges();

			var movimientoCreado = Context.MovimientosEntradaConClub.Local.First();
		    return RedirectToAction("Create", "Pago", new { parentId = movimientoCreado.Id });
		}

		[HttpPost]
	    public ActionResult Anular(int id)
	    {
		    var model = Context.MovimientosEntradaConClub.Find(id);

			VMM.MapAnular(model);

			Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	    [HttpPost]
	    public ActionResult PagarMontoAdeudado(int id)
	    {
		    var model = Context.MovimientosEntradaConClub.Find(id);

		    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));

		    var pago = new Pago
		    {
			    Fecha = DateTime.Today,
			    FechaAlta = DateTime.Now,
			    Importe = model.ImporteAdeudado(),
			    MovimientoEntradaConClubId = model.Id,
			    UsuarioAlta = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()),
			    Vigente = true
		    };

		    Context.Pagos.Add(pago);

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

		public ActionResult Auditoria(int id)
	    {
		    var model = Context.MovimientosEntradaConClub.Find(id);

		    var vm = VMM.MapForDetails(model);

		    return View(vm);
	    }
	}
}