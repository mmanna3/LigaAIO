﻿using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class PublicoController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly TablaWebPublicaBuilder _tablaWebPublicaBuilder;
		private readonly TablaAnualWebPublicaBuilder _tablaAnualWebPublicaBuilder;
		private readonly ZonaHelper _zonaHelper;
		private readonly ImagenesEscudosDiskPersistence _imagenesEscudosPersistence;

		public PublicoController()
		{			
			_context = new ApplicationDbContext();
			_tablaWebPublicaBuilder = new TablaWebPublicaBuilder(_context);
			_tablaAnualWebPublicaBuilder = new TablaAnualWebPublicaBuilder(_context);
			_zonaHelper = new ZonaHelper(_context);
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		}

		public ActionResult Index()
		{
			var webPublicaDistPath = HostingEnvironment.MapPath("~/WebPublica/dist");
			var filePath = Directory.GetFiles(webPublicaDistPath, "*.html").First();
			return Redirect("~/WebPublica/dist/" + Path.GetFileName(filePath));
		}

		public JsonResult Torneos()
		{
			var jornadas = _context.Torneos
				.Where(x => x.Publico)
				.ToList()
				.Select(x => new { descripcion = $"{x.Tipo.Descripcion}", id = x.Id.ToString(), formato = x.Tipo.Formato.ToString().ToLower() })
				.ToList();

			return Json(jornadas, JsonRequestBehavior.AllowGet);
		}

		public JsonResult Zonas(int torneoId)
		{
			var zonas = _context.Zonas
				.Where(x => x.TorneoId == torneoId)
				.GroupBy(x => x.Nombre).Select(y => y.FirstOrDefault())
				.ToList();

			var result = zonas
							.Select(x => new { descripcion = $"{x.Nombre}", id = x.Id.ToString() })
							.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult Posiciones(int zonaAperturaId, string fase)
		{
			var metodo = GetType().GetMethod($"Posiciones{fase}");

			return (ActionResult) metodo.Invoke(this, new object[] { zonaAperturaId });
		}

		public ActionResult PosicionesAnual(int zonaAperturaId)
		{
			var zona = _context.Zonas.Find(zonaAperturaId);		
			var tablas = _tablaAnualWebPublicaBuilder.Tablas(zona);

			return Json(tablas, JsonRequestBehavior.AllowGet);
		}

		public ActionResult PosicionesClausura(int zonaAperturaId)
		{
			var zonaApertura = _context.Zonas.Find(zonaAperturaId);
			var tablas = new TablasVM();
			if (zonaApertura != null)
			{
				var zonaClausura = _zonaHelper.ZonaClausura(zonaApertura);
				tablas = _tablaWebPublicaBuilder.Tablas(zonaClausura);
			}

			return Json(tablas, JsonRequestBehavior.AllowGet);
		}

		public ActionResult PosicionesApertura(int zonaAperturaId)
		{
			var zona = _context.Zonas.Find(zonaAperturaId);
			var tablas = _tablaWebPublicaBuilder.Tablas(zona);

			return Json(tablas, JsonRequestBehavior.AllowGet);
		}

		public ActionResult PosicionesRelampago(int zonaAperturaId)
		{
			var zona = _context.Zonas.Find(zonaAperturaId);
			var tablas = _tablaWebPublicaBuilder.Tablas(zona);

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
	}
}