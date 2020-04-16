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
    public abstract class ABMControllerWithParent<TModel, TVM, TVMM, TParentModel, TParentVM, TParentVMM> : Controller 
		where TModel : class, new()
	    where TVM : ViewModelConId, new()
		where TVMM : CommonVMM<TModel, TVM>
	    where TParentModel : class, new()
	    where TParentVM : ViewModelConId, new()
		where TParentVMM : CommonVMM<TParentModel, TParentVM>
	{
	    protected readonly ApplicationDbContext Context;
	    protected readonly TVMM VMM;
		protected readonly TParentVMM ParentVMM;
		private readonly string _parentIdName;
		private readonly string _controllerName;
		private readonly string _parentControllerName;

		protected ABMControllerWithParent(string parentControllerName, string parentIdName)
		{
		    Context = new ApplicationDbContext();		    
			VMM = (TVMM) Activator.CreateInstance(typeof(TVMM), Context);
			ParentVMM = (TParentVMM)Activator.CreateInstance(typeof(TParentVMM), Context);
			_parentIdName = parentIdName;
			_controllerName = GetType().Name.Replace("Controller","");
			_parentControllerName = parentControllerName;
		}

		public virtual ActionResult Index(int parentId)
		{
			return View(ParentVM(parentId));
		}

		private TParentVM ParentVM(int parentId)
		{
			var parentModel = Context.Set<TParentModel>().Find(parentId);
			return ParentVMM.MapForDetails(parentModel);
		}

		private static object GetPropValue(object src, string propName)
		{
			return src.GetType().GetProperty(propName)?.GetValue(src, null);
		}

		protected virtual int GetParentId(TVM vm)
		{
			return (int)GetPropValue(vm, _parentIdName);
		}

		private static void SetPropValue(object src, string propName, object value)
		{
			src.GetType().GetProperty(propName)?.SetValue(src, value, null);
		}

		[ImportModelStateFromTempData]
	    public virtual ActionResult Create(int parentId)
	    {
			var vm = new TVM();
		    SetPropValue(vm, _parentIdName, parentId);
			BeforeReturningCreateView(vm);

			return View(vm);
		}

		protected virtual void BeforeReturningCreateView(TVM vm)
		{
		}

		[HttpPost, ExportModelStateToTempData]
	    public virtual ActionResult Create(TVM vm)
	    {
			if (!ModelState.IsValid)
				return RedirectTo("Create", GetParentId(vm));

			var model = new TModel();

		    VMM.MapForCreate(vm, model);

		    Context.Set<TModel>().Add(model);

		    BeforeCreteSaving(model);

			Context.SaveChanges();

		    return RedirectTo("Index", GetParentId(vm));
		}

		protected virtual void BeforeCreteSaving(TModel model){}

		protected RedirectToRouteResult RedirectTo(string action, int parentId)
		{
			return RedirectToRoute("ParentWithChild", new { parent = _parentControllerName, parentId, action, controller= _controllerName });
		}

		private RedirectToRouteResult RedirectToAppendingVMId(string action, TVM viewModel)
		{
			return RedirectToRoute("ParentWithChild", new { parent = _parentControllerName, parentId = (int)GetPropValue(viewModel, _parentIdName), action = action, controller = _controllerName, id = viewModel.Id });
		}

		[ImportModelStateFromTempData]
		public virtual ActionResult Edit(int id)
		{
			var model = Context.Set<TModel>().Find(id);

			var vm = VMM.MapForEdit(model);

			return View(vm);
		}

		[HttpPost, ExportModelStateToTempData]
		public virtual ActionResult Edit(TVM vm)
		{
			if (!ModelState.IsValid)
				return RedirectToAppendingVMId("Edit", vm);

			var model = Context.Set<TModel>().Find(vm.Id);

			VMM.MapForEdit(vm, model);

			Context.SaveChanges();

			return RedirectTo("Index", GetParentId(vm));
		}

		public virtual ActionResult Details(int id)
		{
			var model = Context.Set<TModel>().Find(id);

			var vm = VMM.MapForDetails(model);

			return View(vm);
		}

		public virtual JsonResult GetForGrid(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue, int parentId, string filterField, string filterValue, string filterOperator = "==")
	    {
		    var options = new GijgoGridOptions(page, limit, sortBy, direction, searchField, searchValue);

		    IQueryable<TModel> query;
			if (string.IsNullOrEmpty(filterField))
				query = Context.Set<TModel>().Where($"{_parentIdName} == {parentId}").AsQueryable();
			else
				query = Context.Set<TModel>().Where($"{_parentIdName} == {parentId} and {filterField} {filterOperator} {filterValue}").AsQueryable();

			List<TModel> models;

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