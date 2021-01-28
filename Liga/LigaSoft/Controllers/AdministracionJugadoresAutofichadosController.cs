using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using LigaSoft.BusinessLogic;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionJugadoresAutofichadosController : Controller
    {
	    private readonly ApplicationDbContext _context;
	    private readonly JugadorAutofichadoVMM _jugadorAutofichadoVMM;
	    private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;
	    private readonly JugadorVMM _jugadorVMM;
	    private readonly GeneradorDeMovimientos _generadorDeMovimientos;

	    public AdministracionJugadoresAutofichadosController()
		{			
			_context = new ApplicationDbContext();
			_jugadorAutofichadoVMM = new JugadorAutofichadoVMM(_context);
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_jugadorVMM = new JugadorVMM(_context);
			_generadorDeMovimientos = new GeneradorDeMovimientos(_context);
		}

	    public ActionResult Index()
	    {
		    return View();
	    }

	    [HttpPost]
		public ActionResult Aprobar(int id)
		{
			var jugadorAutofichado = _context.JugadoresaAutofichados.Single(x => x.Id == id);

			try
			{
				var jugador = new Jugador();

				var vm = new FicharNuevoJugadorVM
				{
					DNI = jugadorAutofichado.DNI,
					Apellido = jugadorAutofichado.Apellido,
					Nombre = jugadorAutofichado.Nombre,
					EquipoId = jugadorAutofichado.EquipoId,
					FechaNacimiento = DateTimeUtils.ConvertToString(jugadorAutofichado.FechaNacimiento)
				};

				var jugadorEquipo = _jugadorVMM.MapCreate(vm, jugador);
			
				_context.JugadorEquipos.Add(jugadorEquipo);
				jugadorAutofichado.Estado = EstadoJugadorAutofichado.Aprobado;

				var club = _context.Equipos.Find(vm.EquipoId).Club;
				var movimiento = _generadorDeMovimientos.GenerarMovimientoFichajeImpago(club, vm.DNI);
				club.Movimientos.Add(movimiento);

				_context.SaveChanges();

				_imagenesJugadoresDiskPersistence.FicharJugadorTemporal(jugadorAutofichado.DNI);
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error al fichar jugador temporal");
			}

			return RedirectToAction("Index");
		}

		[ExportModelStateToTempData, HttpPost]
		public ActionResult Rechazar(JugadorAutofichadoVM vm)
		{
			if (vm.MotivoDeRechazo.IsEmpty())
			{
				ModelState.AddModelError("", "Al rechazar, el comentario es requerido");
				return RedirectToAction("AprobarRechazar", new {id = vm.Id});
			}
			if (vm.MotivoDeRechazo.Length > 150)
			{
				ModelState.AddModelError("", "El motivo de rechazo no puede tener más de 150 caracteres");
				return RedirectToAction("AprobarRechazar", new { id = vm.Id });
			}

			var jugador = _context.JugadoresaAutofichados.Single(x => x.Id == vm.Id);
			jugador.MotivoDeRechazo = vm.MotivoDeRechazo;
			jugador.Estado = EstadoJugadorAutofichado.Rechazado;

			_context.SaveChanges();
			ModelState.Clear();

			return RedirectToAction("Index");
		}

	    [ImportModelStateFromTempData]
		public ActionResult AprobarRechazar(int id)
	    {
		    var model = _context.JugadoresaAutofichados.Find(id);
		    var vm = _jugadorAutofichadoVMM.MapForEditAndDetails(model);

			return View(vm);
	    }

	}
}