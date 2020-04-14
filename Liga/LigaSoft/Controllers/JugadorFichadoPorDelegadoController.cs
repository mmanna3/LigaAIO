using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Delegado)]
	public class JugadorFichadoPorDelegadoController : CommonController<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM, JugadorFichadoPorDelegadoVMM>
    {
	    [Authorize(Roles = Roles.Delegado)]
		public List<SelectListItem> EquiposParaCombo(Club club)
	    {
		    return club
				.Equipos.OrderBy(x => x.Nombre)
			    .Select(x => new SelectListItem {Text = x.Nombre, Value = x.Id.ToString()})
				.ToList();
	    }

	    private Club ClubDeDelegadoLogueado()
	    {
		    return Context.Clubs.Single(x => x.Id == 10);
	    }

		public ActionResult SeleccionarEquipo()
	    {
		    var club = ClubDeDelegadoLogueado();
		    var vm = new SeleccionarEquipoVM
		    {
				Club = club.Nombre,
				EquiposParaCombo = EquiposParaCombo(club)
		    };

		    return View(vm);
	    }

		[HttpPost]
	    public ActionResult SeleccionarEquipo(SeleccionarEquipoVM vm)
		{
			ViewBag.Equipo = Context.Equipos.Find(vm.EquipoId).Nombre;
			return View("Index");
	    }
	}
}