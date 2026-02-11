using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Otros;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;
using Newtonsoft.Json;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.CualquierEmpleadoDeLaLiga)]
	public class JugadorController : ABMController<Jugador, JugadorBaseVM, JugadorVMM>
    {
	    private readonly ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;

	    public JugadorController()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}

		[HttpPost, ExportModelStateToTempData]
	    public override ActionResult Edit(JugadorBaseVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("Edit", new { id = vm.Id });

		    var model = Context.Jugadores.Find(vm.Id);

		    var hayQueActualizarDNIEnFoto = false;
		    string dniAnterior = null;
			if (model.DNI != vm.DNI)
		    {
				hayQueActualizarDNIEnFoto = true;
			    dniAnterior = model.DNI;
		    }			    

			VMM.MapForEdit(vm, model);
		    Context.SaveChanges();

			if (hayQueActualizarDNIEnFoto)
				_imagenesJugadoresDiskPersistence.CambiarDNI(dniAnterior, model.DNI);

			return RedirectToAction("Index");
		}

		public ActionResult EditFotoWebCam(int id)
	    {
		    var model = Context.Jugadores.Find(id); 

		    var vm = VMM.MapForEditAndDetails(model);

			return View(vm);
	    }

	    [HttpPost]
	    public ActionResult EditFotoWebCam(JugadorBaseVM vm)
	    {
		    if (vm.Foto != null)
				_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(vm);

			return RedirectToAction("Index");
	    }

		[HttpPost, ExportModelStateToTempData]
		public ActionResult EditFotoDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm)
		{
			ValidarEditFotoDesdeArchivo(vm.Foto);
			if (!ModelState.IsValid)
				return RedirectToAction("EditFotoDesdeArchivo", new { id = vm.Id });

			_imagenesJugadoresDiskPersistence.GuardarFotoDeJugadorDesdeArchivo(vm);

		    return RedirectToAction("Index");
	    }

	    [ImportModelStateFromTempData]
		public ActionResult EditFotoDesdeArchivo(int id)
	    {
		    var model = Context.Jugadores.Find(id);

		    var vm = VMM.MapForEditFotoJugadorDesdeArchivo(model);

		    return View(vm);
	    }

	    private void ValidarEditFotoDesdeArchivo(HttpPostedFileBase imagen)
	    {
		    if (imagen == null || imagen.ContentLength == 0)
			    ModelState.AddModelError("", "No se ha seleccionado una imagen.");
		    else if (!"jpg".Equals(imagen.FileName.Substring(imagen.FileName.Length - 3, 3).ToLower()))
			    ModelState.AddModelError("", "La imagen debe estar en formato JPG.");
		    else
			    using (var foto = Image.FromStream(imagen.InputStream))
				    if (foto.Height != 240 || foto.Width != 240)
					    ModelState.AddModelError("", "El tamaño del escudo debe ser de 240 x 240 px.");
	    }

	    [ImportModelStateFromTempData]
	    public ActionResult EliminarJugadores(string mensaje = null)
	    {
		    var vm = new EliminarJugadoresVM
		    {
			    ResultadoEliminacionAnterior = mensaje
		    };
		    
		    return View(vm);
	    }
	    
	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult EliminarJugadores(EliminarJugadoresVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("EliminarJugadores", new { mensaje = "El modelo no es válido"});

		    IQueryable<Jugador> jugadores;
		    IQueryable<JugadorEquipo> jugadoresEquipos;
		    var mensaje = "";
		    switch (vm.Opcion)
		    {
			    case "anio-de-fichaje":
				    var jugadoresEquiposDelAnio = Context.JugadorEquipos.Where(x => x.FechaFichaje.Year == vm.Valor).ToList();
				    // Jugadores que SOLO tienen fichajes de este año → se eliminan por completo
				    // Jugadores que también tienen fichajes de otros años → solo se desvinculan del equipo 2024
				    var idsJugadoresAEliminarPorCompleto = jugadoresEquiposDelAnio
					    .Select(x => x.JugadorId)
					    .Distinct()
					    .Where(jugadorId => !Context.JugadorEquipos.Any(je => je.JugadorId == jugadorId && je.FechaFichaje.Year != vm.Valor))
					    .ToList();
				    var cantidadJugadoresEliminados = idsJugadoresAEliminarPorCompleto.Count;
				    var totalJugadoresConFichajeEnAnio = jugadoresEquiposDelAnio.Select(x => x.JugadorId).Distinct().Count();
				    var cantidadDesvinculaciones = totalJugadoresConFichajeEnAnio - cantidadJugadoresEliminados;
				    Context.JugadorEquipos.RemoveRange(jugadoresEquiposDelAnio);
				    var jugadoresAEliminar = Context.Jugadores.Where(j => idsJugadoresAEliminarPorCompleto.Contains(j.Id));
				    Context.Jugadores.RemoveRange(jugadoresAEliminar);
				    mensaje = cantidadDesvinculaciones > 0
					    ? $"Se eliminaron {cantidadJugadoresEliminados} jugadores y se desvincularon {cantidadDesvinculaciones} jugadores de sus equipos del año {vm.Valor}."
					    : $"Se eliminaron correctamente {cantidadJugadoresEliminados} jugadores fichados en el año {vm.Valor}.";
				    break;
        
			    case "anio-de-nacimiento":
				    jugadores = Context.Jugadores.Where(x => x.FechaNacimiento.Year == vm.Valor);
				    jugadoresEquipos = jugadores.SelectMany(x => x.JugadorEquipo);
				    Context.JugadorEquipos.RemoveRange(jugadoresEquipos);
				    Context.Jugadores.RemoveRange(jugadores);
				    mensaje = $"Se eliminaron correctamente {jugadores.Count()} jugadores nacidos en el año {vm.Valor}";
				    break;
        
			    case "dni":
				    var jugador = Context.Jugadores.Include(jugador1 => jugador1.JugadorEquipo).SingleOrDefault(x => x.DNI == vm.Valor.ToString());
				    
				    if (jugador != null)
				    {
					    Context.JugadorEquipos.RemoveRange(jugador.JugadorEquipo);
					    Context.Jugadores.Remove(jugador);
					    mensaje = $"Se eliminó correctamente el jugador de DNI {vm.Valor}";    
				    }
				    else
				    {
					    mensaje = $"No se encontró el jugador de DNI {vm.Valor}";
				    }
				    
				    break;
        
			    default:
				    return RedirectToAction("EliminarJugadores", new { mensaje = "Valor de opción erróneo"});	    
		    }

		    Context.SaveChanges();

		    return RedirectToAction("EliminarJugadores", new { mensaje });
	    }
	    
	    [ImportModelStateFromTempData]
	    public ActionResult DeshabilitarPorTorneo()
	    {
		    var vm = new DeshabilitarJugadoresPorTorneoVM
		    {
			    TorneoTipos = new List<TextValueItem>()
		    };

		    var torneoTipos = Context.TorneoTipos.Select(x => new TextValueItem
		    {
			    Text = x.Descripcion,
			    Value = x.Id.ToString()
		    });
		    
		    vm.TorneoTipos.AddRange(torneoTipos);

		    return View(vm);
	    }
	    
	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult DeshabilitarPorTorneo(DeshabilitarJugadoresPorTorneoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("DeshabilitarPorTorneo");
		    
		    var jugadoresADeshabilitar = Context.JugadorEquipos
													.Include(x => x.Equipo.Torneo.Tipo)
													.Where(x => x.Equipo.Torneo.Tipo.Id == vm.TorneoTipoId)
													.ToList();

		    foreach (var jug in jugadoresADeshabilitar)
		    {
			    jug.Estado = EstadoJugador.Inhabilitado;
		    }

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }
	    
		[ImportModelStateFromTempData]
	    public ActionResult EliminarJugadorDeUnEquipo(int id)
	    {
		    var vm = new EliminarJugadorDeUnEquipoVM {JugadorId = id};

			return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult EliminarJugadorDeUnEquipo(EliminarJugadorDeUnEquipoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("EliminarJugadorDeUnEquipo", new { id = vm.JugadorId });

		    var jugador = Context.Jugadores.Find(vm.JugadorId);

		    var equipoAEliminar = jugador?.JugadorEquipo.Single(x => x.EquipoId == vm.EquipoId);
			Context.JugadorEquipos.Remove(equipoAEliminar);

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }

	    public JsonResult EliminarJugadorDeUnEquipoAutocomplete(int jugadorId, string term = "")
	    {		    
		    var result = Context.JugadorEquipos
			    .Where(x => x.JugadorId == jugadorId)
				.ToList()
			    .Select(c => new IdDescripcionVM
			    {
				    Id = c.Equipo.Id,
				    Descripcion = c.Equipo.Descripcion()
			    })
				.ToList();

		    return Json(result, JsonRequestBehavior.AllowGet);
	    }

		/// <summary>
		/// Método que va a llamar la impresora para imprimir el carnet
		/// Ejemplo-http://localhost:58657/Jugador/GetJugador?jugadorId=5088&equipoId=149
		/// </summary>
		[AllowAnonymous]
	    public string Getjugador(int jugadorId, int equipoId)
	    {
			var jugador = Context.Jugadores.Find(jugadorId);
		    var equipo = Context.Equipos.Find(equipoId);

			var vm = VMM.MapJugadorParaCarnet(jugador, equipo);

			return JsonConvert.SerializeObject(vm);
	    }

		public JsonResult GetByEquipoId(GijgoGridOptions options, int parentId)
		{
			var query = Context.JugadorEquipos
								.Where($"EquipoId == {parentId}")
								.AsQueryable();

			//query = GijgoGridHelper.ApplyOptionsToQuery(query, options, out int total);

			var records = VMM.MapForImprimirJugadores(query.ToList());

			return Json(new { records, total = records.Count }, JsonRequestBehavior.AllowGet);
		}

	    [ImportModelStateFromTempData]
	    public ActionResult FicharEnOtroEquipo(int id)
	    {
		    var nombre = Context.Jugadores.Find(id).Descripcion();

			var vm = new FicharEnOtroEquipoVM {JugadorId = id, JugadorNombre = nombre};

		    return View(vm);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public ActionResult FicharEnOtroEquipo(FicharEnOtroEquipoVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("FicharEnOtroEquipo", new { id = vm.JugadorId });

		    var equipo = Context.Equipos.Find(vm.NuevoEquipoId);
		    var jugador = Context.Jugadores.Find(vm.JugadorId);

		    equipo?.JugadorEquipo.Add(new JugadorEquipo { Equipo = equipo, Jugador = jugador, FechaFichaje = DateTime.Today, Estado = EstadoJugador.Activo});

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }

	    public JsonResult FicharEnOtroEquipoAutocomplete(int jugadorId, string term = "")
	    {
		    var torneosQueEstaJugando = Context.JugadorEquipos.Where(x => x.JugadorId == jugadorId).Select(y => y.Equipo.TorneoId);

			var equiposDeTorneosQueNoEstaJugando = Context.EquiposActivos()
				.Where(x => !torneosQueEstaJugando.Contains(x.TorneoId) && x.Nombre.Contains(term))
			    .ToList()
			    .Select(c => new IdDescripcionVM
			    {
				    Id = c.Id,
				    Descripcion = c.Descripcion()
			    })
			    .ToList();

		    return Json(equiposDeTorneosQueNoEstaJugando, JsonRequestBehavior.AllowGet);
	    }

		[HttpPost]
		public ActionResult CambiarEstado(int equipoId, int jugadorId, EstadoJugador estadoId)
		{
			var jugadorEquipo = Context.JugadorEquipos.Single(x => x.JugadorId == jugadorId && x.EquipoId == equipoId);

			jugadorEquipo.Estado = estadoId;

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult ActualizarTarjetas(int equipoId, int jugadorId, int tarjetasAmarillas, int tarjetasRojas)
		{
			var jugadorEquipo = Context.JugadorEquipos.Single(x => x.JugadorId == jugadorId && x.EquipoId == equipoId);

			jugadorEquipo.TarjetasAmarillas = tarjetasAmarillas;
			jugadorEquipo.TarjetasRojas = tarjetasRojas;

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}