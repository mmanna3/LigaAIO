using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class PublicController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly WebPublicaVMM _webPublicaVMM;

		public PublicController()
		{
			_context = new ApplicationDbContext();
			_webPublicaVMM = new WebPublicaVMM(_context);
		}

		public ActionResult AppInit()
		{
			var vm = PublicIndexVM(null);

			return View("Index", vm);
		}

		private PublicIndexVM PublicIndexVM(Zona zona)
		{
			var vm = _webPublicaVMM.MapIndex(zona);
			return vm;
		}

		public ActionResult Clubes(int id)
		{
			var zona = _context.Zonas.Find(id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapDatosDeEquiposWebPublica(zona, vm);			

			return View(vm);
		}

		public ActionResult Posiciones(int id)
		{
			var zona = _context.Zonas.Find(id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapPosicionesWebPublica(zona, vm);

			if (zona.VerGolesEnTabla)
				return View("PosicionesConGoles",vm);
			return View("PosicionesSinGoles", vm);
		}


		public ActionResult PosicionesAnual(int idZonaApertura)
		{
			var zonaApertura = _context.Zonas.Find(idZonaApertura);			

			var vm = PublicIndexVM(zonaApertura);

			_webPublicaVMM.MapPosicionesAnualesWebPublica(zonaApertura, vm);

			if (zonaApertura.VerGolesEnTabla)
				return View("PosicionesConGoles", vm);
			return View("PosicionesSinGoles", vm);
		}

		public ActionResult Jornadas(int id)
		{
			var zona = _context.Zonas.Find(id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapJornadasWebPublica(zona, vm);

			return View(vm);
		}

		public ActionResult Fixture(int id)
		{
			var zona = _context.Zonas.Include(x => x.Fechas).FirstOrDefault(x => x.Id == id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapFixtureWebPublica(zona, vm);

			return View(vm);
		}

		public ActionResult Sanciones(int id)
		{
			var zona = _context.Zonas.Include(x => x.Fechas).FirstOrDefault(x => x.Id == id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapSancionesWebPublica(zona, vm);

			return View(vm);
		}

		public ActionResult Goleadores(int id)
		{
			var zona = _context.Zonas.Find(id);

			var vm = PublicIndexVM(zona);

			_webPublicaVMM.MapGoleadoresWebPublica(zona, vm);

			return View(vm);
		}
	}
}