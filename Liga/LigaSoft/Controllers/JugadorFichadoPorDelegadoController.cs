using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.Otros;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.AdmininstradorYDelegado)]
	public class JugadorFichadoPorDelegadoController : ABMController<JugadorAutofichado, JugadorAutofichadoVM, JugadorAutofichadoVMM>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public JugadorFichadoPorDelegadoController()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
		}

		public ActionResult PendientesDeAprobacion(IdDescripcionVM vm)
		{
			return View("PendientesDeAprobacion", vm);
		}

		public ActionResult Rechazados(IdDescripcionVM vm)
		{
			return View("Rechazados", vm);
		}

		public ActionResult Aprobados(IdDescripcionVM vm)
		{
			var equipo = Context.Equipos.Find(vm.Id);

			var equipoVMM = new EquipoVMM(Context);

			var equipoVM = equipoVMM.MapForDetails(equipo);

			return View("Aprobados", equipoVM);
		}
		
		public List<SelectListItem> EquiposParaCombo(Club club)
	    {
		    return club
				.EquiposActivos()
				.OrderBy(x => x.Nombre)
			    .Select(x => new SelectListItem {Text = $"{x.Nombre} - {x.Torneo.Tipo.Descripcion}", Value = x.Id.ToString()})
				.ToList();
	    }

	    private Club ClubDeDelegadoLogueado()
	    {		    
		    var aspNetUser = _userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
		    return Context.UsuariosDelegados.Single(x => x.AspNetUserId == aspNetUser.Id).Club;
	    }
	    
		public ActionResult SeleccionarEquipo(string alSeleccionarIrAAction)
	    {
		    var club = ClubDeDelegadoLogueado();
		    var vm = new SeleccionarEquipoVM
		    {
				Club = club.Nombre,
				EquiposParaCombo = EquiposParaCombo(club),
			    AlSeleccionarIrAAction = alSeleccionarIrAAction
			};

		    return View(vm);
	    }

		//[ImportModelStateFromTempData]
		//public ActionResult Fichar(int equipoId)
	 //   {
		//    var vm = new JugadorFichadoPorDelegadoVM
		//    {
		//		Equipo = Context.Equipos.Find(equipoId).Nombre,
		//		EquipoId = equipoId
		//    };

		//	return View(vm);
	 //   }

		//[HttpPost, ExportModelStateToTempData]
		//public ActionResult Fichar(JugadorFichadoPorDelegadoVM vm)
		//{
		//	try
		//	{
		//		ValidarFotos(vm);
		//		if (!ModelState.IsValid || JugadorYaEstaFichado(vm.DNI))
		//			return Fichar(vm.EquipoId);

		//		var model = new JugadorFichadoPorDelegado();
		//		VMM.MapForCreateAndEdit(vm, model);
		//		Context.JugadoresFichadosPorDelegados.Add(model);
		//		Context.SaveChanges();

		//		_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorFichadoPorDelegado(vm);
		//	}
		//	catch (Exception e)
		//	{
		//		YKNExHandler.LoguearYLanzarExcepcion(e, "Error cuando el delegado intenta fichar el jugador");
		//	}

		//	return RedirectToAction("PendientesDeAprobacion", new IdDescripcionVM
		//	{
		//		Descripcion = Context.Equipos.Find(vm.EquipoId).Nombre,
		//		Id = vm.EquipoId
		//	});
		//}

		//private void ValidarFotos(JugadorAutofichadoVM vm)
		//{
		//	ValidarFotoCarnet(vm);
		//	ValidarFotoDNIFrente(vm);
		//}

		//private void ValidarFotoCarnet(JugadorAutofichadoVM vm)
		//{
		//	if (string.IsNullOrEmpty(vm.FotoCarnet))
		//		ModelState.AddModelError("", "Debe seleccionar una foto carnet.");
		//}

		//private void ValidarFotoDNIFrente(JugadorAutofichadoVM vm)
		//{
		//	if (vm.FotoDNIFrente == null || vm.FotoDNIFrente.ContentLength == 0)
		//		ModelState.AddModelError("", "Debe seleccionar una foto DNI Frente.");
		//	else
		//		ValidarExtensionFotoDNIFrente(vm);
		//}

		private void ValidarExtensionFotoDNIFrente(JugadorAutofichadoVM vm)
		{
			if (!"jpg".Equals(vm.ArchivoDeFotoDNIFrente.FileName.Substring(vm.ArchivoDeFotoDNIFrente.FileName.Length - 3, 3).ToLower()) &&
			    !"jpeg".Equals(vm.ArchivoDeFotoDNIFrente.FileName.Substring(vm.ArchivoDeFotoDNIFrente.FileName.Length - 4, 4).ToLower()))
				ModelState.AddModelError("", "La foto DNI Frente debe estar en formato JPG o JPEG.");
		}

		[ImportModelStateFromTempData]
		public ActionResult EditarJugadorRechazado(int id)
		{
			var model = Context.JugadoresaAutofichados.Find(id);

			var vm = VMM.MapForEdit(model);

			return View(vm);
		}

		[HttpPost, ExportModelStateToTempData]
		public ActionResult EditarJugadorRechazado(JugadorAutofichadoVM vm)
		{
			try
			{
				if (vm.ArchivoDeFotoDNIFrente != null)
					ValidarExtensionFotoDNIFrente(vm);

				if (!ModelState.IsValid)
					return RedirectToAction("Edit", vm.Id);

				var model = Context.JugadoresaAutofichados.Find(vm.Id);

				var dniAnterior = model.DNI;

				VMM.MapForEdit(vm, model);

				Context.SaveChanges();

				_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorAutofichadoSiendoEditado(vm);

				if (dniAnterior != vm.DNI)
					_imagenesJugadoresDiskPersistence.RenombrarFotosTemporalesPorCambioDeDNI(dniAnterior, vm.DNI);
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error al editar jugador fichado por delegado y rechazado.");				
				
				return RedirectToAction("Rechazados", new IdDescripcionVM
				{
					
					Descripcion = Context.Equipos.Find(vm.EquipoId).Nombre,
					Id = vm.EquipoId
				});
			}

			return RedirectToAction("PendientesDeAprobacion", new IdDescripcionVM
			{
				Descripcion = Context.Equipos.Find(vm.EquipoId).Nombre,
				Id = vm.EquipoId
			});
		}

		private bool JugadorYaEstaFichado(string dni)
	    {
		    var result = Context.Jugadores.Any(x => x.DNI == dni) || Context.JugadoresaAutofichados.Any(x => x.DNI == dni);
			ModelState.AddModelError("", "El jugador ya se encuentra fichado.");
		    return result;
	    }

	    [HttpPost]
	    public ActionResult SeleccionarEquipo(SeleccionarEquipoVM seleccionarEquipoVM)
	    {
		    if (string.IsNullOrEmpty(seleccionarEquipoVM.AlSeleccionarIrAAction))
			    seleccionarEquipoVM.AlSeleccionarIrAAction = "PendientesDeAprobacion";

			return RedirectToAction(seleccionarEquipoVM.AlSeleccionarIrAAction, new IdDescripcionVM
			{
				Descripcion = Context.Equipos.Find(seleccionarEquipoVM.EquipoId).Nombre,
				Id = seleccionarEquipoVM.EquipoId
			});
	    }

		//public virtual JsonResult GetAprobados(string equipoId)
		//{
		//	equipoId = equipoId.Substring(0, 1); //Porque viene con un extraño signo de pregunta al final

		//	var query = Context.Equipos.Find(Convert.ToInt32(equipoId));

		//	var query = Context.JugadoresaAutofichados.Where(x => (int)x.Estado == estadoInt); estado = estado.Substring(0, 1);

		//	var records = VMM.MapForGrid(query.ToList());

		//	var cantidad = 0;
		//	if (records != null)
		//		cantidad = records.Count;

		//	return Json(new { records, cantidad }, JsonRequestBehavior.AllowGet);
		//}
	}
}