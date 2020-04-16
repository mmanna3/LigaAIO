using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Utilidades;
using Microsoft.AspNet.Identity.Owin;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionJugadoresFichadosPorDelegadosController : Controller
    {
	    private readonly ApplicationDbContext _context;

		public AdministracionJugadoresFichadosPorDelegadosController()
		{			
			_context = new ApplicationDbContext();
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

		public ActionResult Rechazar(int id)
		{
			//var jugador = _context.JugadoresFichadosPorDelegados.Single(x => x.Id == id);
			//_context.JugadoresFichadosPorDelegados.Remove(jugador);
			//comentario
			//_context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}