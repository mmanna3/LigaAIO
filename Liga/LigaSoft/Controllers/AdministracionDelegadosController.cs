using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionDelegadosController : CommonController<UsuarioDelegadoSinConfirmar, UsuarioDelegadoSinConfirmarVM, UsuarioDelegadoSinConfirmarVMM>
    {
	    private ApplicationUserManager _userManager;

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

	   // public async Task<ActionResult> AprobarUsuario()
	   // {
		  //  if (ModelState.IsValid)
		  //  {
			 //   var user = new ApplicationUser { UserName = "usuaria", Email = "usuaria@usuario.com" };
			 //   var result = await UserManager.CreateAsync(user, "password");
			 //   if (result.Succeeded)
			 //   {
				//	await UserManager.AddToRoleAsync(user.Id, Roles.Delegado);
				//}
		  //  }

		  //  return Json("", JsonRequestBehavior.AllowGet);
	   // }
	}
}