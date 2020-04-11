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
	[Route("Delegados")] //VER QUÉ ONDA
	public class UsuarioDelegadoController : CommonController<Categoria, CategoriaVM, CategoriaVMM>
    {
		[AllowAnonymous]
		public ActionResult Registrar()
	    {
		    var vm = new UsuarioDelegadoSinConfirmarVM {ClubsParaCombo = ClubsParaCombo()};
		    return View(vm);
		}

	    [AllowAnonymous]
		[HttpPost]
		public ActionResult Registrar(UsuarioDelegadoSinConfirmarVM vm)
	    {
		    if (!ModelState.IsValid)
		    {
			    vm.ClubsParaCombo = ClubsParaCombo();
				return View(vm);
			}

		    return View("RegistroExitoso");
	    }

		public List<SelectListItem> ClubsParaCombo()
	    {
		    return Context.Clubs.ToComboValues();
	    }
	}
}