using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.AdmininstradorYDelegado)]
	public class JugadorFichadoPorDelegadoController : CommonController<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM, JugadorFichadoPorDelegadoVMM>
    {
	    [Authorize(Roles = Roles.Delegado)]
		public List<SelectListItem> EquiposParaCombo(int clubId)
	    {
		    return Context.Clubs.Single(x => x.Id == clubId)
				.Equipos.OrderBy(x => x.Nombre)
			    .Select(x => new SelectListItem {Text = x.Nombre, Value = x.Id.ToString()})
				.ToList();
	    }

	    private int ClubDeDelegadoLogueado()
	    {
		    return 10;
	    }

	    [Authorize(Roles = Roles.Delegado)]
		public ActionResult SeleccionarEquipo()
	    {
		    var clubId = ClubDeDelegadoLogueado();
		    var vm = new SeleccionarEquipoVM
		    {
			    EquiposParaCombo = EquiposParaCombo(clubId)
		    };

		    return View(vm);
	    }
    }
}