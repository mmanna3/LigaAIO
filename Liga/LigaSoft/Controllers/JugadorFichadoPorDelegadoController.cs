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
	[Authorize(Roles = Roles.AdmininstradorYDelegado)]
	public class JugadorFichadoPorDelegadoController : CommonController<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM, JugadorFichadoPorDelegadoVMM>
    {
	    [Authorize(Roles = Roles.Delegado)]
		public List<SelectListItem> EquiposParaCombo()
	    {
		    var id = ClubDeDelegadoLogueado();
		    return Context.Clubs.Single(x => x.Id == id).Equipos.AsQueryable().OrderBy(x => x.Nombre).ToComboValues();			
	    }

	    private int ClubDeDelegadoLogueado()
	    {
		    throw new System.NotImplementedException();
	    }
    }
}