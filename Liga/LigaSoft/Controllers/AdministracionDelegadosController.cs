﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Otros;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity.Owin;

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

	    public ActionResult Index()
	    {
		    return View();
	    }

		public async Task<ActionResult> Aprobar(int id)
		{
			var usuarioDelegado = _context.UsuariosDelegadosPendientesDeAprobacion.Single(x => x.Id == id);

			var user = new ApplicationUser { UserName = usuarioDelegado.Email, Email = usuarioDelegado.Email };
			var result = await UserManager.CreateAsync(user, usuarioDelegado.Password);

			if (result.Succeeded)
			{
				await UserManager.AddToRoleAsync(user.Id, Roles.Delegado);
				usuarioDelegado.AspNetUserId = user.Id;
				_context.SaveChanges();
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}