using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.AdmininstradorYDelegado)]
	public class JugadorFichadoPorDelegadoController : ABMController<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM, JugadorFichadoPorDelegadoVMM>
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public JugadorFichadoPorDelegadoController()
		{
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
			if (!ModelState.IsValid || JugadorYaEstaFichado(vm.DNI))
				return Fichar(vm.EquipoId);

			var model = new JugadorFichadoPorDelegado();
			VMM.MapForCreateAndEdit(vm, model);
			Context.JugadoresFichadosPorDelegados.Add(model);
			Context.SaveChanges();

			return RedirectToAction("PendientesDeAprobacion", new IdDescripcionVM
			{
				Descripcion = Context.Equipos.Find(vm.EquipoId).Nombre,
				Id = vm.EquipoId
			});
		}

		public ActionResult Crop()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Crop(string imagen)
		{
			var bitmap = ImagenUtility.ProcesarFotoBase64ParaGuardarEnDisco(imagen);
			bitmap.Save("C:\\Imagen.jpg");

			return View();
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
			return RedirectToAction(seleccionarEquipoVM.AlSeleccionarIrAAction, new IdDescripcionVM
			{
				Descripcion = Context.Equipos.Find(seleccionarEquipoVM.EquipoId).Nombre,
				Id = seleccionarEquipoVM.EquipoId
			});
	    }
	}
}