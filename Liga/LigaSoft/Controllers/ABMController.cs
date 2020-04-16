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

		public virtual JsonResult GetForGrid(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue, string filterField, string filterValue, string filterOperator = "==")
		{
		    var options = new GijgoGridOptions(page, limit, sortBy, direction, searchField, searchValue);

		    var query = Context.Set<TModel>().AsQueryable();

			List<TModel> models;

			if (!string.IsNullOrEmpty(filterField))
				query = Context.Set<TModel>().Where($"{filterField} {filterOperator} {filterValue}").AsQueryable();

			if (!string.IsNullOrWhiteSpace(options.SearchValue))
			    query = query.Where($"{options.SearchField}.Contains(@0)", options.SearchValue);

		    if (!string.IsNullOrEmpty(options.SortBy) && !string.IsNullOrEmpty(options.Direction))
			    query = query.OrderBy(options.Direction.Trim().ToLower() == "asc" ? options.SortBy : $"{options.SortBy} descending");
		    else
			    query = query.OrderBy("Id descending");

		    var total = query.Count();
		    if (options.Page.HasValue && options.Limit.HasValue)
		    {
			    var start = (options.Page.Value - 1) * options.Limit.Value;
			    models = query.Skip(start).Take(options.Limit.Value).ToList();
		    }
		    else
			    models = query.ToList();

		    var records = VMM.MapForGrid(models);

		    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }
	}
}