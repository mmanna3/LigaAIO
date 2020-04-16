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

			if (!string.IsNullOrEmpty(options.filterField))
			{
				if (options.filterOperator == null)
					options.filterOperator = "==";

				query = Context.Set<TModel>().Where($"{options.filterField} {options.filterOperator} {options.filterValue}").AsQueryable();
			}				

			if (!string.IsNullOrWhiteSpace(options.searchValue))
			    query = query.Where($"{options.searchField}.Contains(@0)", options.searchValue);

		    if (!string.IsNullOrEmpty(options.sortBy) && !string.IsNullOrEmpty(options.direction))
			    query = query.OrderBy(options.direction.Trim().ToLower() == "asc" ? options.sortBy : $"{options.sortBy} descending");
		    else
			    query = query.OrderBy("Id descending");

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

		public class GijgoGridOpciones
		{
			public int? page { get; set; }
			public int? limit { get; set; }
			public string sortBy { get; set; }
			public string direction { get; set; }
			public string searchField { get; set; }
			public string searchValue { get; set; }
			public string filterField { get; set; }
			public string filterValue { get; set; }
			public string filterOperator { get; set; }
		}		
	}
}