using System;
using System.IO;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class JugadorAutofichadoController : ABMController<JugadorAutofichado, JugadorAutofichadoVM, JugadorAutofichadoVMM>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public JugadorAutofichadoController()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
		}

		public ActionResult Aprobados(IdDescripcionVM vm)
		{
			return View("Index", vm);
		}

		[HttpPost, AllowAnonymous]
		public JsonResult Autofichaje()
		{
			try
			{
				var vm = CastearRequestAJugadorAutofichadoVM();
				var model = new JugadorAutofichado();
				VMM.MapForCreateAndEdit(vm, model);
				Context.JugadoresaAutofichados.Add(model);
				Context.SaveChanges();

				_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorAutofichado(vm);
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error en autofichaje.");
				return Json("Error", JsonRequestBehavior.AllowGet);
			}

			return Json("OK", JsonRequestBehavior.AllowGet);
		}

		private JugadorAutofichadoVM CastearRequestAJugadorAutofichadoVM()
		{
			//ASP NET no se banca un json tan grande (son 3 fotos en base64)
			string json;
			using (var reader = new StreamReader(HttpContext.Request.InputStream))
			{
				json = reader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<JugadorAutofichadoVM>(json);
		}
	}
}