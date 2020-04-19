using System;

using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.DiskPersistence;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.CualquierEmpleadoDeLaLiga)]
	public class EquipoController : ABMController<Equipo, EquipoVM, EquipoVMM>
    {
	    private readonly JugadorVMM _jugadorVMM;
	    private readonly int _valorPorDefectoEnPesosDelConceptoFichaje;
	    private readonly ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;

		public EquipoController()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_valorPorDefectoEnPesosDelConceptoFichaje = Context.ParametrizacionesGlobales.Select(x => x.ValorPorDefectoEnPesosDelConceptoFichaje).First();
			_jugadorVMM = new JugadorVMM(Context);
			
		}

		public ActionResult ImportarJugadores(int id)
		{
			var vm = new ImportarJugadoresVM {EquipoEnElQueLoEstoyFichandoId = id};

			return View(vm);
		}

	    public ActionResult ImprimirJugadores(int id)
	    {
		    var equipo = Context.Equipos.Find(id);

		    var vm = new ImprimirJugadoresVM {Equipo = equipo.Nombre, EquipoId = id};

			return View(vm);
	    }

	    public ActionResult Pases(int id)
	    {
		    var equipo = Context.Equipos.Find(id);

		    var vm = new PasesVM { EquipoOrigen = equipo.Nombre, EquipoOrigenId = id };

		    return View(vm);
	    }

	    [HttpPost]
		public ActionResult Pases(PasesVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("Pases", new { id = vm.EquipoOrigenId });

			var equipo = Context.Equipos.Find(vm.EquipoDestinoId);
		    foreach (var jugId in vm.JugadoresSeleccionados)
		    {
			    var jugEquipoOld = Context.JugadorEquipos.Single(x => x.EquipoId == vm.EquipoOrigenId && x.JugadorId == jugId);				
			    Context.JugadorEquipos.Remove(jugEquipoOld);

			    var jugEquipoNew = new JugadorEquipo {EquipoId = equipo.Id, JugadorId = jugId, FechaFichaje = jugEquipoOld.FechaFichaje};
			    Context.JugadorEquipos.Add(jugEquipoNew);
			}			    

			Context.SaveChanges();

			return RedirectToAction("Index");
		}

	    public JsonResult PasesEquipoDestinoAutocomplete(int equipoOrigenId, string term = "")
	    {
		    var torneoId = Context.Equipos.Find(equipoOrigenId).TorneoId;

			var result = Context.Equipos
			    .Where(x => x.TorneoId == torneoId && x.Id != equipoOrigenId)
			    .ToList()
				.Select(c => new IdDescripcionVM
				{
					Id = c.Id,
					Descripcion = c.Descripcion()
				})
				.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
	    }

		[HttpPost]
		public ActionResult ImportarJugadores(ImportarJugadoresVM vm)
		{
			var jugadores = ZipUtility.Importar(vm.JugadoresZip.InputStream, out var resultado);
			var equipo = Context.Equipos.Find(vm.EquipoEnElQueLoEstoyFichandoId);

			var cont = 0;
			foreach (var jug in jugadores)
				try
				{
					var jugadorEquipo = new JugadorEquipo {Equipo = equipo, Jugador = jug, FechaFichaje = DateTime.Today};

					Context.JugadorEquipos.Add(jugadorEquipo);
					Context.SaveChanges();					

					resultado.Add($"Jugador de DNI '{jug.DNI}' importado exitosamente.");
					cont++;
				}
				catch (Exception e)
				{
					IODiskUtility.EliminarFotoDeJugador(jug.DNI);
					var message = e.InnerException?.InnerException == null ? e.Message : e.InnerException.InnerException.Message;
					resultado.Add($"Error con jugador de DNI '{jug.DNI}': {message}");
				}

			resultado.Add($"Se importaron correctamente {cont} jugadores.");

			var vmResultado = new ResultadoImportacionJugadores
			{
				Equipo = equipo?.Nombre,
				Resultado = resultado
			};

			return View("ResultadoImportacion", vmResultado);
		}

	    [ImportModelStateFromTempData]
	    public ActionResult FicharNuevoJugador(int id, int? idDelJugadorFichadoAnteriormenteParaImprimir = null)
	    {
			var equipo = Context.Equipos.Find(id);

			var vm = new FicharNuevoJugadorVM {
				EquipoId = id,
				Equipo = equipo.Nombre,
				LabelGenerarMovimientoFichaje = $"Se abonaron ${_valorPorDefectoEnPesosDelConceptoFichaje} correspondientes al carnet",
				IdDelJugadorFichadoAnteriormenteParaImprimir = idDelJugadorFichadoAnteriormenteParaImprimir
			};

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult FicharNuevoJugador(FicharNuevoJugadorVM vm)
	    {
		    ValidarFichajeDeNuevoJugador(vm);
			if (!ModelState.IsValid)
				return RedirectToAction("FicharNuevoJugador", new { id = vm.EquipoId });

			var model = new Jugador();

		    var jugadorEquipo = _jugadorVMM.MapCreate(vm, model);

		    Context.JugadorEquipos.Add(jugadorEquipo);

		    if (vm.ElCarnetEstaPago)
			    GenerarMovimientoFichajeYSuPago(vm.EquipoId, vm.DNI);
			else
			    GenerarMovimientoFichajeImpago(vm.EquipoId, vm.DNI);

		    Context.SaveChanges();
		    _imagenesJugadoresDiskPersistence.GuardarFotoWebCam(vm);

			ModelState.Clear();

			int? idDelJugadorRecienFichado = null;
		    if (vm.HayQueImprimirCarnetDelUltimoJugadorFichado)
				idDelJugadorRecienFichado = Context.Jugadores.Single(x => x.DNI == vm.DNI).Id;

			return RedirectToAction("FicharNuevoJugador", new {id = vm.EquipoId, idDelJugadorFichadoAnteriormenteParaImprimir = idDelJugadorRecienFichado });
	    }

	    private void GenerarMovimientoFichajeYSuPago(int equipoId, string dni)
	    {
		    var movimiento = GenerarMovimientoFichaje(equipoId, dni);
		    GenerarPagoDelMovimientoFichaje(dni, movimiento);
	    }

	    private void GenerarMovimientoFichajeImpago(int equipoId, string dni)
	    {
		    GenerarMovimientoFichaje(equipoId, dni);		    
	    }

		private void GenerarPagoDelMovimientoFichaje(string dni, MovimientoEntradaConClub movimiento)
	    {
		    var pago = new Pago
		    {
			    Movimiento = movimiento,
			    Comentario = $"Pago generado automáticamente al fichar al jugador con DNI:{dni}",
			    Fecha = DateTime.Now,
			    FechaAlta = DateTime.Now,
			    Vigente = true,
			    UsuarioAltaId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
			    Importe = movimiento.Total
		    };

		    Context.Pagos.Add(pago);
	    }

	    private MovimientoEntradaConClub GenerarMovimientoFichaje(int equipoId, string dni)
	    {
		    var club = Context.Equipos.Find(equipoId).Club;

		    var movimiento = new MovimientoEntradaConClub
		    {
			    ConceptoId = (int)ConceptoTipoEnum.Fichaje,
			    ClubId = club.Id,
			    Cantidad = 1,
			    Comentario = $"Movimiento generado automáticamente al fichar al jugador con DNI:{dni}",
			    Fecha = DateTime.Now,
			    FechaAlta = DateTime.Now,
			    PrecioUnitario = _valorPorDefectoEnPesosDelConceptoFichaje,
			    Total = _valorPorDefectoEnPesosDelConceptoFichaje,
			    Vigente = true,
			    UsuarioAltaId = System.Web.HttpContext.Current.User.Identity.GetUserId()
		    };

		    club.Movimientos.Add(movimiento);

		    return movimiento;
	    }

	    private void ValidarFichajeDeNuevoJugador(JugadorBaseVM vm)
	    {
		    if (Context.Jugadores.Any(x => x.DNI == vm.DNI))
				ModelState.AddModelError("", "Ya hay un jugador fichado con el mismo DNI.");
	    }

		protected override void AfterEditMapping(EquipoVM vm)
	    {
		    vm.TorneosParaCombo = TorneosParaCombo();
		    vm.Delegados = DelegadosCombo(vm.ClubId);
		}

	    public List<SelectListItem> TorneosParaCombo()
	    {
		    return Context.Torneos.Include(y => y.Tipo).Where(x => x.Publico).ToComboValues();
	    }

	    public List<SelectListItem> DelegadosCombo(int clubId)
	    {
		    return Context.Delegados.Where(y => y.ClubId == clubId).ToComboValuesAgregandoBlancoAlPrincipio();
	    }

		public JsonResult FicharJugadorExistenteAutocomplete(int equipoId, string term = "")
	    {
		    var equipo = Context.Equipos.Find(equipoId);
			var jugadoresDeEquiposDeEstaLiga = Context.JugadorEquipos
													.Where(x => x.Equipo.TorneoId == equipo.TorneoId)
													.Select(x => x.Jugador)
													.ToList();

		    var result = Context.Jugadores.ToList()
							.Except(jugadoresDeEquiposDeEstaLiga)
							.Select(c => new IdDescripcionVM
							{
								Id = c.Id,
								Descripcion = c.Descripcion()
							})
							.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
	    }

	    public override ActionResult Edit(int id)
	    {
		    var model = Context.Equipos.Find(id);

		    var vm = VMM.MapForEdit(model);

		    AfterEditMapping(vm);

		    return View(vm);
	    }
	}
}