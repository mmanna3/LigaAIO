using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity.Owin;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionJugadoresFichadosPorDelegadosController : Controller
    {
	    private readonly ApplicationDbContext _context;
	    private readonly JugadorFichadoPorDelegadoVMM _jugadorFichadoPorDelegadoVMM;
	    private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;
	    private readonly JugadorVMM _jugadorVMM;

	    public AdministracionJugadoresFichadosPorDelegadosController()
		{			
			_context = new ApplicationDbContext();
			_jugadorFichadoPorDelegadoVMM = new JugadorFichadoPorDelegadoVMM(_context);
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_jugadorVMM = new JugadorVMM(_context);
		}

	    public ActionResult JugadoresPendientesDeAprobacion()
	    {
		    return View();
	    }

		public ActionResult Aprobar(int id)
		{
			var jugadorFichadoPorDelegado = _context.JugadoresFichadosPorDelegados.Single(x => x.Id == id);

			try
			{
				var jugador = new Jugador();

				var vm = new FicharNuevoJugadorVM
				{
					DNI = jugadorFichadoPorDelegado.DNI,
					Apellido = jugadorFichadoPorDelegado.Apellido,
					Nombre = jugadorFichadoPorDelegado.Nombre,
					EquipoId = jugadorFichadoPorDelegado.EquipoId,
					FechaNacimiento = DateTimeUtils.ConvertToString(jugadorFichadoPorDelegado.FechaNacimiento)
				};

				var jugadorEquipo = _jugadorVMM.MapCreate(vm, jugador);
			
				_context.JugadorEquipos.Add(jugadorEquipo);
				jugadorFichadoPorDelegado.Estado = EstadoJugadorFichadoPorDelegado.Aprobado;
				
				//GenerarMovimientoFichajeImpago(vm.EquipoId, vm.DNI); Resolvé esto mono

				_context.SaveChanges();

				_imagenesJugadoresDiskPersistence.FicharJugadorTemporal(jugadorFichadoPorDelegado.DNI);
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error al fichar jugador temporal");
			}

			return RedirectToAction("JugadoresPendientesDeAprobacion");
		}

		[ExportModelStateToTempData, HttpPost]
		public ActionResult Rechazar(JugadorFichadoPorDelegadoVM vm)
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

			var jugador = _context.JugadoresFichadosPorDelegados.Single(x => x.Id == vm.Id);
			jugador.MotivoDeRechazo = vm.MotivoDeRechazo;
			jugador.Estado = EstadoJugadorFichadoPorDelegado.Rechazado;

			_context.SaveChanges();

			return RedirectToAction("JugadoresPendientesDeAprobacion");
		}

	    [ImportModelStateFromTempData]
		public ActionResult AprobarRechazar(int id)
	    {
		    var model = _context.JugadoresFichadosPorDelegados.Find(id);
		    var vm = _jugadorFichadoPorDelegadoVMM.MapForEditAndDetails(model);

			return View(vm);
	    }

	}
}