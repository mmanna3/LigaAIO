using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity.Owin;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionJugadoresFichadosPorDelegadosController : Controller
    {
	    private readonly ApplicationDbContext _context;
	    private readonly JugadorFichadoPorDelegadoVMM _jugadorFichadoPorDelegadoVMM;

	    public AdministracionJugadoresFichadosPorDelegadosController()
		{			
			_context = new ApplicationDbContext();
			_jugadorFichadoPorDelegadoVMM = new JugadorFichadoPorDelegadoVMM(_context);
		}

	    public ActionResult JugadoresPendientesDeAprobacion()
	    {
		    return View();
	    }

		public ActionResult Aprobar(int id)
		{
			var jugadoresFichadosPorDelegados = _context.JugadoresFichadosPorDelegados.Single(x => x.Id == id);

			//var user = new ApplicationUser { UserName = usuarioDelegado.Email, Email = usuarioDelegado.Email };
			//var result = await UserManager.CreateAsync(user, usuarioDelegado.Password);

			//if (result.Succeeded)
			//{
			//	await UserManager.AddToRoleAsync(user.Id, Roles.Delegado);
			//	usuarioDelegado.AspNetUserId = user.Id;
			//	usuarioDelegado.Aprobado = true;
			//	_context.SaveChanges();
			//}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[ExportModelStateToTempData, HttpPost]
		public ActionResult Rechazar(JugadorFichadoPorDelegadoVM vm)
		{
			if (vm.MotivoDeRechazo.IsEmpty())
			{
				ModelState.AddModelError("", "Al rechazar, el comentario es requerido");
				return RedirectToAction("AprobarRechazar", new {id = vm.Id});
			}

			var jugador = _context.JugadoresFichadosPorDelegados.Single(x => x.Id == vm.Id);
			jugador.MotivoDeRechazo = vm.MotivoDeRechazo;
			jugador.Estado = EstadoJugadorFichadoPorDelegado.Rechazado;

			_context.SaveChanges();

			return RedirectToAction("JugadoresPendientesDeAprobacion");
		}

	    [ImportModelStateFromTempData]
		public ActionResult AprobarRechazar(int id)
	    {
		    var model = _context.JugadoresFichadosPorDelegados.Find(id);
		    var vm = _jugadorFichadoPorDelegadoVMM.MapForEditAndDetails(model);

			return View(vm);
	    }

	}
}