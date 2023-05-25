using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Otros;
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
	[Authorize(Roles = Roles.Administrador), AllowCrossSite]
	public class JugadorAutofichadoController : ABMController<JugadorAutofichado, JugadorAutofichadoVM, JugadorAutofichadoVMM>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public JugadorAutofichadoController()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
		}

		[HttpPost, AllowAnonymous]
		public JsonResult Autofichaje()
		{
			try
			{
				var vm = CastearRequestAJugadorAutofichadoVM();
				var model = new JugadorAutofichado();
				VMM.MapForCreateAndEdit(vm, model);

				SiElDNISeHabiaFichadoYEstaRechazadoEliminarElAnterior(model.DNI);

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

		private void SiElDNISeHabiaFichadoYEstaRechazadoEliminarElAnterior(string dni)
		{
			var jugador = Context.JugadoresaAutofichados.SingleOrDefault(x => x.DNI == dni);
			if (jugador != null)
				Context.JugadoresaAutofichados.Remove(jugador);
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

		public JsonResult GetForGrido(string estado)
		{
			estado = estado.Substring(0, 1); //Porque viene con un extraño signo de pregunta al final

			var estadoInt = Convert.ToInt32(estado);

			var query = Context.JugadoresaAutofichados.Where(x => (int)x.Estado == estadoInt); estado = estado.Substring(0, 1); //Porque viene con un extraño signo de pregunta al final
			
			var records = VMM.MapForGrid(query.ToList());

			var cantidad = 0;
			if (records != null)
				cantidad = records.Count;

			return Json(new { records, cantidad}, JsonRequestBehavior.AllowGet);
		}
	}
}