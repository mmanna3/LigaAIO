using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using Microsoft.AspNet.Identity.Owin;
using static LigaSoft.Models.ViewModels.DelegadosPorClubVM;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionDelegadosController : Controller
    {
	    private ApplicationUserManager _userManager;
	    private readonly ApplicationDbContext _context;

		public AdministracionDelegadosController()
		{			
			_context = new ApplicationDbContext();
		}

		public ApplicationUserManager UserManager
	    {
		    get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
		    private set => _userManager = value;
	    }

	    protected override void Dispose(bool disposing)
	    {
		    if (disposing)
		    {
			    if (_userManager != null)
			    {
				    _userManager.Dispose();
				    _userManager = null;
			    }
		    }

		    base.Dispose(disposing);
	    }

	    public ActionResult DelegadosPendientesDeAprobacion()
	    {
		    return View();
	    }

	    public ActionResult DelegadosAprobados()
	    {
		    return View();
	    }

		public async Task<ActionResult> Aprobar(int id)
		{
			var usuarioDelegado = _context.UsuariosDelegados.Single(x => x.Id == id);

			var user = new ApplicationUser { UserName = usuarioDelegado.Usuario, Email = $"{usuarioDelegado.Usuario}@edefi.com.ar" };
			var result = await UserManager.CreateAsync(user, usuarioDelegado.Password);

			if (result.Succeeded)
			{
				await UserManager.AddToRoleAsync(user.Id, Roles.Delegado);
				usuarioDelegado.AspNetUserId = user.Id;
				usuarioDelegado.Aprobado = true;
				_context.SaveChanges();
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Rechazar(int id)
		{
			var usuarioDelegado = _context.UsuariosDelegados.Single(x => x.Id == id);
			_context.UsuariosDelegados.Remove(usuarioDelegado);
			_context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
		
		public ActionResult BlanquearClave(string usuario)
		{
			var appUser = _context.Users.Single(x => x.UserName == usuario);
			var usuarioDelegado = _context.UsuariosDelegados.SingleOrDefault(x => x.AspNetUserId == appUser.Id);
			if (usuarioDelegado != null)
				usuarioDelegado.BlanqueoDeClavePendiente = true;
			
			_context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		public ActionResult DelegadosPorClub()
		{
			var clubs = _context.Clubs.Include(x =>
				x.UsuariosDelegados.Select(usuarioDelegado => usuarioDelegado.AspNetUser)).Include(club => club.Delegados).OrderBy(x => x.Nombre).ToList();

			var vm = new DelegadosPorClubVM { Lista = new List<DelegadoPorClubVM>() };

			foreach (var club in clubs)
			{
				if (club.Delegados != null && club.UsuariosDelegados.Count > 0)
				{
					foreach (var delegado in club.UsuariosDelegados)
					{
						vm.Lista.Add(new DelegadoPorClubVM
						{
							Club = delegado.Club.Nombre,
							Estado = delegado.Aprobado ? "Aprobado" : "Pendiente",
							Nombre = delegado.Nombre + " " + delegado.Apellido,
							Usuario = delegado.AspNetUser.UserName,
							BlanqueoDeClavePendiente = delegado.BlanqueoDeClavePendiente.ToSiNoString()
						});
					}
				} else
				{
					vm.Lista.Add(new DelegadoPorClubVM { Club = club.Nombre, Estado = "No tiene" });
				}
			}

			return View(vm);
		}
	}
}