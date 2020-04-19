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
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class ClubController : ABMController<Club, ClubVM, ClubVMM>
    {
	    private readonly EquipoVMM _equipoVMM;
	    private IImagenesEscudosPersistence _imagenesEscudosPersistence;

	    public ClubController()
	    {
		    _equipoVMM = new EquipoVMM(Context);
		    _imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
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
			    _imagenesEscudosPersistence.Eliminar(id);

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

			_imagenesEscudosPersistence.Guardar(vm);

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

	    public JsonResult DelegadosGrid(GijgoGridOptions options, int parentId)
		{
		    var query = Context.Delegados
			    .Where($"ClubId == {parentId}")
			    .AsQueryable();

		    query = GijgoGridHelper.ApplyOptionsToQuery(query, options, out int total);

			var records = VMM.MapForDelegadosGrid(query.ToList());

			return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }
	}
}