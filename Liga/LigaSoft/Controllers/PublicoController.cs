using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Migrations;
using LigaSoft.Models;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;
using ZonaTipo = LigaSoft.Models.Enums.ZonaTipo;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class PublicoController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly TablaWebPublicaBuilder _tablaWebPublicaBuilder;
		private readonly TablaAnualWebPublicaBuilder _tablaAnualWebPublicaBuilder;
		private readonly ImagenesEscudosDiskPersistence _imagenesEscudosPersistence;
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public PublicoController()
		{			
			_context = new ApplicationDbContext();
			_tablaWebPublicaBuilder = new TablaWebPublicaBuilder(_context);
			_tablaAnualWebPublicaBuilder = new TablaAnualWebPublicaBuilder(_context);
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}

		public ActionResult Index()
		{
			var webPublicaDistPath = HostingEnvironment.MapPath("~/WebPublica/dist");
			var filePath = Directory.GetFiles(webPublicaDistPath, "*.html").First();
			return Redirect("~/WebPublica/dist/" + Path.GetFileName(filePath));
		}

		public JsonResult TorneosAperturaClausura(string anio)
		{
			Enum.TryParse(anio, out Anio anioEnum);

			var result = _context.Torneos
				.Where(x => x.Publico && x.Tipo.Formato == TorneoFormato.AperturaClausura && x.Anio == anioEnum)
				.ToList()
				.Select(x => new { descripcion = $"{x.Tipo.Descripcion}", id = x.Id.ToString(), formato = "aperturaclausura" })
				.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ObtenerNombreDelEquipo(int equipoId)
		{
			var result = _context.Equipos
				.SingleOrDefault(x => x.Id == equipoId)?
				.Nombre;

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult TorneosRelampago(string anio)
		{
			Enum.TryParse(anio, out Anio anioEnum);

			var result = _context.Torneos
				.Where(x => x.Publico && x.Tipo.Formato == TorneoFormato.Relampago && x.Anio == anioEnum)
				.ToList()
				.Select(x => new { descripcion = $"{x.Tipo.Descripcion}", id = x.Id.ToString(), formato = "relampago" })
				.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult Fichar(string apellido)
		{
			//_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorAutofichado(vm);
			return Json("OK", JsonRequestBehavior.AllowGet);
		}

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

		public JsonResult Zonas(int torneoId)
		{		
			var zonas = _context.Zonas
				.Where(x => x.TorneoId == torneoId)				
				.ToList();

			var result = new List<ZonaFE>();

			foreach (var zona in zonas)
			{
				if (result.All(x => x.descripcion != zona.Nombre))
					result.Add(new ZonaFE {descripcion = zona.Nombre});

				var zonaFE = result.Single(x => x.descripcion == zona.Nombre);

				switch (zona.Tipo)
				{
					case ZonaTipo.Apertura:
						zonaFE.zonaAperturaId = zona.Id;
						break;
					case ZonaTipo.Clausura:
						zonaFE.zonaClausuraId = zona.Id;
						break;
					case ZonaTipo.Relampago:
						zonaFE.zonaRelampagoId = zona.Id;
						break;
				}
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Posiciones(int zonaId)
		{
			var zona = _context.Zonas.Find(zonaId);
			var tablas = _tablaWebPublicaBuilder.Tablas(zona);

			return Json(tablas, JsonRequestBehavior.AllowGet);
		}

		public ActionResult PosicionesAnual(int zonaAperturaId)
		{
			var zona = _context.Zonas.Find(zonaAperturaId);		
			var tablas = _tablaAnualWebPublicaBuilder.Tablas(zona);

			return Json(tablas, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Jornadas(int zonaId)
		{
			var zona = _context.Zonas.Find(zonaId);
			var result = new ResumenDeJornadasVM();

			if (zona != null)
			{
				var resumenJornadasHelper = new ResumenDeJornadasBuilder();
				var fechas = zona.Fechas.Where(x => x.Publicada).ToList();
				result = resumenJornadasHelper.Tablas(zona, fechas);
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Clubes(int zonaId)
		{
			var zona = _context.Zonas.Find(zonaId);

			var result = new DatosDeEquiposVM("");
			var zonaHelper = new ZonaHelper(_context);

			foreach (var equipo in zonaHelper.EquiposDeLaZonaDatosParaLosDatosWebPublica(zona))
			{
				var renglon = new RenglonDatosEquipo
				{
					Equipo = equipo.Nombre,
					Escudo = _imagenesEscudosPersistence.PathRelativo(equipo.Club.Id),
					Direccion = equipo.Club.Direccion,
					Localidad = equipo.Club.Localidad,
					TechoDescripcion = equipo.Club.TechoBoolToTechoEnum().Descripcion(),
				};

				result.Renglones.Add(renglon);
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Fixture(int zonaId)
		{
			var zona = _context.Zonas.Include(x => x.Fechas).FirstOrDefault(x => x.Id == zonaId);
			var result = new FixtureVM("");

			if (zona != null)
			{
				//Estas dos líneas deberían estar adentro del MapFixture
				result.ZonaId = zona.Id;
				result.PublicadoBool = zona.FixturePublicado;

				var zonaVMM = new ZonaVMM(_context);

				if (zona.FixturePublicado)
					zonaVMM.MapFixture(zona, result);
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Noticias()
		{
			var result = _context.Noticias
							.Where(x => x.Visible)
							.OrderByDescending(x => x.Fecha)
							.ToList()
							.Select(x => new { id = x.Id, titulo = x.Titulo, subtitulo = x.Subtitulo, fecha = $"{x.Fecha:d-M}" });

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Noticia(int id)
		{
			var noticia = _context.Noticias.Find(id);
			var result = new { id = noticia.Id, titulo = noticia.Titulo, subtitulo = noticia.Subtitulo, fecha = $"{noticia.Fecha:d-M}", cuerpo = noticia.Cuerpo };
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		private class ZonaFE
		{
			public string descripcion { get; set; }
			public int? zonaAperturaId { get; set; }
			public int? zonaClausuraId { get; set; }
			public int? zonaRelampagoId { get; set; }
		}
	}
}