using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Otros;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Newtonsoft.Json;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Todos)]
	public class JugadorController : CommonController<Jugador, JugadorBaseVM, JugadorVMM>
    {
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
				IODiskUtility.ActualizarDNIEnFoto(dniAnterior, model.DNI);

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
			    IODiskUtility.GuardarFotoWebCamDeJugadorEnDisco(vm);

		    return RedirectToAction("Index");
	    }

		[HttpPost, ExportModelStateToTempData]
		public ActionResult EditFotoDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm)
		{
			ValidarEditFotoDesdeArchivo(vm.Foto);
			if (!ModelState.IsValid)
				return RedirectToAction("EditFotoDesdeArchivo", new { id = vm.Id });

			IODiskUtility.GuardarFotoDeJugadorDesdeArchivoEnDisco(vm);

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

		public virtual JsonResult GetByEquipoId(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue, int equipoId)
		{
			var options = new GijgoGridOptions(page, limit, sortBy, direction, searchField, searchValue);

			var query = Context.JugadorEquipos
								.Where($"EquipoId == {equipoId}")
								.Select(x => x.Jugador)
								.AsQueryable();

			List<Jugador> models;

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

		    equipo?.JugadorEquipo.Add(new JugadorEquipo { Equipo = equipo, Jugador = jugador, FechaFichaje = DateTime.Today});

		    Context.SaveChanges();

		    return RedirectToAction("Index");
	    }

	    public JsonResult FicharEnOtroEquipoAutocomplete(int jugadorId, string term = "")
	    {
		    var torneosQueEstaJugando = Context.JugadorEquipos.Where(x => x.JugadorId == jugadorId).Select(y => y.Equipo.TorneoId);

			var equiposDeTorneosQueNoEstaJugando = Context.Equipos				
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
	}
}