using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Migrations;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;
using Newtonsoft.Json;
using ZonaTipo = LigaSoft.Models.Enums.ZonaTipo;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	[AllowCrossSite]
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
			//return View(); DESCOMENTAR ESTO CUANDO ANDE EL FICHAJE
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

		public JsonResult ObtenerNombreDelEquipo(string codigoAlfanumerico)
		{
			var equipoId = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos(codigoAlfanumerico);

			var result = _context.Equipos
				.SingleOrDefault(x => x.Id == equipoId)?
				.Nombre;

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public JsonResult ElDniEstaFichado(string dni)
		{
			var result = _context.Jugadores.Any(x => x.DNI == dni);

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

		public JsonResult Publicidades()
		{
			var publicidades = _context.Publicidades
				.Where(x => x.Posicion == PublicidadPosicion.IzquierdaSuperior || x.Posicion == PublicidadPosicion.DerechaSuperior)
				.ToList();

			var result = publicidades.Select(x => new { id = x.Id, titulo = $"{x.Titulo}", imgSrc = $"/Imagenes/Publicidades/{x.Id.ToString()}.jpg", urlDestino = $"{x.Url}" })
				.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}

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

		public ActionResult Sanciones(int zonaId)
		{
			var zona = _context.Zonas.Include(x => x.Torneo).Single(x => x.Id == zonaId);

			if (!zona.Torneo.SancionesHabilitadas)
				return Json(new ArrayList(), JsonRequestBehavior.AllowGet);

			var result = new List<RenglonSancion>();
			var sanciones = _context.Sanciones.Where(x => x.Jornada.Fecha.ZonaId == zona.Id && x.Visible).ToList();

			foreach (var sancion in sanciones)
			{
				if (sancion.Visible)
				{
					var renglon = new RenglonSancion
					{
						sancion = sancion.Descripcion,
						dia = DateTimeUtils.ConvertToString(sancion.Dia),
						fecha = sancion.Jornada.Fecha.Numero.ToString(),
						local = sancion.Jornada.NombreDelLocal(),
						visitante = sancion.Jornada.NombreDelVisitante(),
						categoria = sancion.Categoria.Nombre,
						fechasQueAdeuda = sancion.CantidadFechasQueAdeuda
					};

					result.Add(renglon);
				}
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

	public class RenglonSancion
	{
		public string dia { get; set; }
		public string fecha { get; set; }
		public string local { get; set; }
		public string visitante { get; set; }
		public string categoria { get; set; }
		public string sancion { get; set; }
		public int fechasQueAdeuda { get; set; }
	}
}