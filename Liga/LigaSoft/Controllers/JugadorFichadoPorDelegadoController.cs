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

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.AdmininstradorYDelegado)]
	public class JugadorFichadoPorDelegadoController : ABMController<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM, JugadorFichadoPorDelegadoVMM>
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
			return View("Aprobados", vm);
		}

		[Authorize(Roles = Roles.Delegado)]
		public List<SelectListItem> EquiposParaCombo(Club club)
	    {
		    return club
				.Equipos.OrderBy(x => x.Nombre)
			    .Select(x => new SelectListItem {Text = x.Nombre, Value = x.Id.ToString()})
				.ToList();
	    }

	    private Club ClubDeDelegadoLogueado()
	    {		    
		    var aspNetUser = _userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
		    return Context.UsuariosDelegados.Single(x => x.AspNetUserId == aspNetUser.Id).Club;
	    }

		[Authorize(Roles = Roles.Delegado)]
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

	    public ActionResult Fichar(int equipoId)
	    {
		    var vm = new JugadorFichadoPorDelegadoVM
		    {
				Equipo = Context.Equipos.Find(equipoId).Nombre,
				EquipoId = equipoId
		    };

			return View(vm);
	    }

		[HttpPost]
	    public ActionResult Fichar(JugadorFichadoPorDelegadoVM vm)
		{
			try
			{
				if (!ModelState.IsValid || JugadorYaEstaFichado(vm.DNI))	//Tenga todos los campos
					return Fichar(vm.EquipoId);

				var model = new JugadorFichadoPorDelegado();
				VMM.MapForCreateAndEdit(vm, model);
				Context.JugadoresFichadosPorDelegados.Add(model);
				Context.SaveChanges();

				_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorFichadoPorDelegado(vm);
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error cuando el delegado intenta fichar el jugador");
			}

			return RedirectToAction("PendientesDeAprobacion", new IdDescripcionVM
			{
				Descripcion = Context.Equipos.Find(vm.EquipoId).Nombre,
				Id = vm.EquipoId
			});
		}

		[ImportModelStateFromTempData]
		public override ActionResult Edit(int id)
		{
			var model = Context.JugadoresFichadosPorDelegados.Find(id);

			var vm = VMM.MapForEdit(model);

			return View(vm);
		}

		[HttpPost, ExportModelStateToTempData]
		public override ActionResult Edit(JugadorFichadoPorDelegadoVM vm)
		{
			try
			{
				if (!ModelState.IsValid)
					return RedirectToAction("Edit");

				var model = Context.JugadoresFichadosPorDelegados.Find(vm.Id);

				VMM.MapForEdit(vm, model);

				Context.SaveChanges();

				_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorFichadoPorDelegado(vm);
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
		    var result = Context.Jugadores.Any(x => x.DNI == dni) || Context.JugadoresFichadosPorDelegados.Any(x => x.DNI == dni);
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
	}
}