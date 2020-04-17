using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Otros;
using LigaSoft.Models.ViewModels;
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

			List<TModel> models;

			if (options.filters != null)
				foreach (var filter in options.filters)
					query = query.Where($"{filter.field} {filter.@operator} {filter.value}").AsQueryable();

			if (!string.IsNullOrEmpty(options.filterField))
			{
				if (options.filterOperator == null)
					options.filterOperator = "==";

				query = query.Where($"{options.filterField} {options.filterOperator} {options.filterValue}").AsQueryable();
			}				

			if (!string.IsNullOrWhiteSpace(options.searchValue))
			    query = query.Where($"{options.searchField}.Contains(@0)", options.searchValue);

		    if (!string.IsNullOrEmpty(options.sortBy) && !string.IsNullOrEmpty(options.direction))
			    query = query.OrderBy(options.direction.Trim().ToLower() == "asc" ? options.sortBy : $"{options.sortBy} desc");
		    else
			    query = query.OrderBy("Id desc");

		    var total = query.Count();
		    if (options.page.HasValue && options.limit.HasValue)
		    {
			    var start = (options.page.Value - 1) * options.limit.Value;
			    models = query.Skip(start).Take(options.limit.Value).ToList();
		    }
		    else
			    models = query.ToList();

		    var records = VMM.MapForGrid(models);

		    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }		
	}
}