using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Otros;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class ClubController : CommonController<Club, ClubVM, ClubVMM>
    {
	    private readonly EquipoVMM _equipoVMM;

	    public ClubController()
	    {
		    _equipoVMM = new EquipoVMM(Context);
	    }

		[ImportModelStateFromTempData]
	    public ActionResult CrearEquipo(int id)
	    {
		    var equipoController = DependencyResolver.Current.GetService<EquipoController>();
		    equipoController.ControllerContext = new ControllerContext(Request.RequestContext, equipoController);

			var vm = new EquipoVM
		    {
			    ClubId = id,
			    TorneosParaCombo = equipoController.TorneosParaCombo(),
			    Delegados = DelegadosCombo(id)
			};

		    return View(vm);
	    }

	    public List<SelectListItem> DelegadosCombo(int clubId)
	    {
			return Context.Delegados.Where(y => y.ClubId == clubId).ToComboValuesAgregandoBlancoAlPrincipio();
	    }

		[HttpPost, ExportModelStateToTempData]
	    public ActionResult CrearEquipo(EquipoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("CrearEquipo", new {id = vm.ClubId});

		    var model = new Equipo();

		    _equipoVMM.MapCreate(vm, model);

		    Context.Equipos.Add(model);

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }

	    public ActionResult IndexDelegados(int id)
	    {
		    var model = Context.Clubs.Find(id);

		    var vm = VMM.MapForEditAndDetails(model);

			return View(vm);
	    }

	    [HttpPost]
	    public ActionResult EliminarEscudo(int id)
	    {
		    if (Context.Clubs.Find(id) != null)
			    IODiskUtility.EliminarEscudo(id);

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

		[ImportModelStateFromTempData]
		public ActionResult CargarEscudo(int id)
	    {
		    var club = Context.Clubs.Find(id);
		    var vm = new CargarEscudoVM {Titulo = $"Cargar escudo del club {club.Nombre}", ClubId = club.Id};

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult CargarEscudo(CargarEscudoVM vm)
	    {
		    ValidarCargarEscudo(vm.Escudo);
			if (!ModelState.IsValid)
			    return RedirectToAction("CargarEscudo", new { id = vm.ClubId });

			IODiskUtility.GuardarFotoDeEscudoEnDisco(vm);

			Context.SaveChanges();

			return RedirectToAction("Index");
		}

	    private void ValidarCargarEscudo(HttpPostedFileBase imagen)
	    {
			if (imagen == null || imagen.ContentLength == 0)
				ModelState.AddModelError("", "No se ha seleccionado un escudo.");
			else if (!"jpg".Equals(imagen.FileName.Substring(imagen.FileName.Length - 3, 3).ToLower()))
				ModelState.AddModelError("", "La imagen debe estar en formato JPG.");
			else
				using (var foto = System.Drawing.Image.FromStream(imagen.InputStream))
					if (foto.Height != 100 || foto.Width != 100)
						ModelState.AddModelError("", "El tamaño del escudo debe ser de 100 x 100 px.");
		}

		[ImportModelStateFromTempData]
	    public ActionResult CrearDelegado(int id)
		{
			var club = Context.Clubs.Find(id);

			var vm = new DelegadoVM
			{
				Id = id,
				Club = club.Nombre,
				ClubId = club.Id,
			};

			return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult CrearDelegado(DelegadoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("CrearDelegado", new { id = vm.ClubId });

		    var club = Context.Clubs.Find(vm.ClubId);

		    var model = new Delegado
		    {
			    Club = club,
			    Descripcion = vm.Descripcion,
			    Telefono = vm.Telefono
		    };

		    Context.Delegados.Add(model);

		    Context.SaveChanges();

		    return RedirectToAction("IndexDelegados", new { id = vm.ClubId });
		}

	    [ImportModelStateFromTempData]
	    public ActionResult EditarDelegado(int id)
	    {
		    var delegado = Context.Delegados.Find(id);
		    var club = Context.Clubs.Single(x => x.Delegados.Select(y => y.Id).Contains(delegado.Id));

		    var vm = new DelegadoVM
		    {
			    Id = id,
				Telefono = delegado.Telefono,
				Descripcion = delegado.Descripcion,
			    Club = club.Nombre,
			    ClubId = club.Id,
		    };

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult EditarDelegado(DelegadoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("EditarDelegado", new { id = vm.Id });

		    var delegado = Context.Delegados.Find(vm.Id);

		    delegado.Telefono = vm.Telefono;
		    delegado.Descripcion = vm.Descripcion;

			Context.SaveChanges();

		    return RedirectToAction("IndexDelegados", new { id = vm.ClubId });
	    }

		public virtual JsonResult DelegadosGrid(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue, int clubId)
	    {
		    var options = new GijgoGridOptions(page, limit, sortBy, direction, searchField, searchValue);

		    var query = Context.Delegados
			    .Where($"ClubId == {clubId}")
			    .AsQueryable();

		    List<Delegado> models;

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

		    var records = VMM.MapForDelegadosGrid(models);

		    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }
	}
}