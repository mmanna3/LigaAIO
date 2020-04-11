using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Delegado)]
	public class UsuarioDelegadoController : CommonController<UsuarioDelegadoPendienteDeAprobacion, UsuarioDelegadoPendienteDeAprobacionVM, UsuarioDelegadoPendienteDeAprobacionVMM>
    {
		[AllowAnonymous]
		public ActionResult Registrar()
	    {
		    var vm = new UsuarioDelegadoPendienteDeAprobacionVM {ClubsParaCombo = ClubsParaCombo()};
		    return View(vm);
		}

	    [AllowAnonymous]
		[HttpPost]
		public ActionResult Registrar(UsuarioDelegadoPendienteDeAprobacionVM vm)
	    {
		    if (!ModelState.IsValid || EmailYaEstaEnUso(vm.Email))
		    {
			    vm.ClubsParaCombo = ClubsParaCombo();
				return View(vm);
			}

			var model = new UsuarioDelegadoPendienteDeAprobacion();
			VMM.MapForCreate(vm, model);
		    Context.UsuariosDelegadosSinConfirmar.Add(model);
		    Context.SaveChanges();

		    return View("RegistroExitoso");
	    }

	    private bool EmailYaEstaEnUso(string email)
	    {
		    if (Context.UsuariosDelegadosSinConfirmar.Any(x => x.Email == email))
		    {
			    ModelState.AddModelError("", "Debe esperar que la organización de la liga habilite su usuario.");
			    return true;
			}
			if (Context.Users.Any(x => x.Email == email))
		    {
				ModelState.AddModelError("", "El email ya está en uso.");
				return true;
		    }
		    return false;
	    }


	    public List<SelectListItem> ClubsParaCombo()
	    {
		    return Context.Clubs.ToComboValues();
	    }
	}
}