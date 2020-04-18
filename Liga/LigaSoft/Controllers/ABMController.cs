using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Otros;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	public abstract class ABMController<TModel, TViewModel, TVMM> : Controller 
		where TModel : class, new() 
		where TVMM : CommonVMM<TModel, TViewModel>
	    where TViewModel : ViewModelConId, new()
	{
	    protected readonly ApplicationDbContext Context;
	    protected readonly TVMM VMM;

		protected ABMController()
		{
		    Context = new ApplicationDbContext();		    
			VMM = (TVMM) Activator.CreateInstance(typeof(TVMM), Context);
		}

		public virtual ActionResult Index()
        {
			return View();
        }

	    [ImportModelStateFromTempData]
	    public virtual ActionResult Create()
	    {
		    return View();
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public virtual ActionResult Create(TViewModel viewModel)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("Create");

		    var model = new TModel();

		    VMM.MapForCreate(viewModel, model);

		    Context.Set<TModel>().Add(model);

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }

	    [ImportModelStateFromTempData]
	    public virtual ActionResult Edit(int id)
	    {
		    var model = Context.Set<TModel>().Find(id);

		    var vm = VMM.MapForEdit(model);

		    AfterEditMapping(vm);

			return View(vm);
	    }

		protected virtual void AfterEditMapping(TViewModel vm)
		{
		}

		[HttpPost, ExportModelStateToTempData]
	    public virtual ActionResult Edit(TViewModel viewModel)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("Edit");

			var model = Context.Set<TModel>().Find(viewModel.Id);

			VMM.MapForEdit(viewModel, model);

			Context.SaveChanges();

			return RedirectToAction("Index");
	    }

		public virtual ActionResult Details(int id)
		{
			var model = Context.Set<TModel>().Find(id);

			var vm = VMM.MapForDetails(model);

			return View(vm);
		}

		public virtual JsonResult GetForGrid(GijgoGridOpciones options)
		{
			var query = Context.Set<TModel>().AsQueryable();

			query = GijgoGridHelper.ApplyOptionsToQuery(query, options, out int total);

			var records = VMM.MapForGrid(query.ToList());

		    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }
	}
}