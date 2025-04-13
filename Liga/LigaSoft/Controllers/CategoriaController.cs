using System;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
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

	    private bool LosAniosSonInvalidos(CategoriaVM vm)
	    {
		    if (vm.AnioNacimientoDesde == null && vm.AnioNacimientoHasta == null)
			    return false;

		    if (vm.AnioNacimientoDesde == null || vm.AnioNacimientoHasta == null)
		    {
			    ModelState.AddModelError("", "Los dos años son requeridos.");
			    return true;
		    }
		    
		    if (vm.AnioNacimientoDesde.Value > vm.AnioNacimientoHasta.Value)
		    {
			    ModelState.AddModelError("", "El año desde no puede ser mayor al año hasta");
			    return true;
		    }

		    var anioActual = DateTime.Now.Year;
		    if (vm.AnioNacimientoDesde.Value > anioActual || vm.AnioNacimientoHasta.Value > anioActual)
		    {
			    ModelState.AddModelError("", "Los años de nacimiento no pueden ser mayores al año actual");
			    return true;
		    }

		    return false;
	    }
	    
	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Create(CategoriaVM vm)
	    {
		    if (!ModelState.IsValid || LosAniosSonInvalidos(vm))
			    return RedirectToAction("Create", new {parentId = vm.TorneoId});

		    var model = new Categoria();

		    VMM.MapForCreate(vm, model);

		    Context.Categorias.Add(model);

		    Context.SaveChanges();

		    return RedirectTo("Index", vm.TorneoId);
	    }
	    
	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Edit(CategoriaVM vm)
	    {
		    if (!ModelState.IsValid || LosAniosSonInvalidos(vm))
			    return RedirectToAction("Edit", new { id = vm.Id });

		    var model = Context.Categorias.Single(x => x.Id == vm.Id);

		    VMM.MapForEdit(vm, model);

		    Context.SaveChanges();

		    return RedirectTo("Index", vm.TorneoId);
	    }
	}
}