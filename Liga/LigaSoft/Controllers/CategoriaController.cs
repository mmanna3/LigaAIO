using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class CategoriaController : ABMControllerWithParent<Categoria, CategoriaVM, CategoriaVMM, Torneo, TorneoVM, TorneoVMM>
    {
	    public CategoriaController() : base("Torneo","TorneoId")
	    {
	    }

	    protected override void BeforeReturningCreateView(CategoriaVM vm)
	    {
		    vm.Torneo = Context.Torneos.Find(vm.TorneoId)?.Descripcion;
	    }
	}
}