using System;
using System.Collections.Generic;
using System.Linq;
using LigaSoft.Builders;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class WebPublicaVMM
	{
		private readonly ApplicationDbContext _context;

		public WebPublicaVMM(ApplicationDbContext context)
		{
			_context = context;
		}

		public PublicIndexVM MapIndex(Zona zona)
		{
			var torneos = _context.Torneos.Where(x => x.Publico).ToList();
			var noticias = _context.Noticias.Where(x => x.Visible).ToList();
			var publicidades = _context.Publicidades.ToList();

			var vm = new PublicIndexVM();
			MapTorneos(vm, torneos);
			MapNoticias(vm, noticias);
			MapPublicidades(vm, publicidades);
			MapLinksSeleccionadosAnteriormente(vm, zona);

			return vm;
		}

		private static void MapLinksSeleccionadosAnteriormente(PublicIndexVM vm, Zona zona)
		{
			if (zona != null)
			{
				vm.TorneoSeleccionadoId = $"Torneo{zona.TorneoId}Link";
				vm.AnioSeleccionadoId = $"Anio{zona.Torneo.Anio.Descripcion()}Link";

				if (zona.Tipo != ZonaTipo.Relampago)
					vm.AperturaClausuraSeleccionadoId = $"{zona.Tipo.Descripcion()}{zona.TorneoId}Link";

				vm.ZonaSeleccionadaId = $"Zona{zona.Id}Link";
			}
		}

		private void MapNoticias(PublicIndexVM vm, IEnumerable<Noticia> noticias)
		{
			var noticiaVMM = new NoticiaVMM(_context);
			var noticiasAux = new List<NoticiaVM>();
			foreach (var noticia in noticias)
			{
				if (noticia.Visible)
				{
					var noticiaVM = noticiaVMM.MapForDetails(noticia);
					noticiasAux.Add(noticiaVM);
				}
			}
			vm.Noticias = noticiasAux.OrderByDescending(x => DateTimeUtils.ConvertToDateTime(x.Fecha)).ToList();
		}

		private static void MapPublicidades(PublicIndexVM vm, IList<Publicidad> publicidades)
		{
			var pubIzqSup = publicidades.Single(x => x.Posicion == PublicidadPosicion.IzquierdaSuperior);
			var pubIzqInf = publicidades.Single(x => x.Posicion == PublicidadPosicion.IzquierdaInferior);
			var pubDerSup = publicidades.Single(x => x.Posicion == PublicidadPosicion.DerechaSuperior);
			var pubDerInf = publicidades.Single(x => x.Posicion == PublicidadPosicion.DerechaInferior);

			vm.Publicidades = new PublicidadesVM
			{
				IzquierdaSuperiorPath = pubIzqSup.ImagenPath(),
				IzquierdaSuperiorUrl = pubIzqSup.Url,
				IzquierdaInferiorPath = pubIzqInf.ImagenPath(),
				IzquierdaInferiorUrl = pubIzqInf.Url,
				DerechaSuperiorPath = pubDerSup.ImagenPath(),
				DerechaSuperiorUrl = pubDerSup.Url,
				DerechaInferiorPath = pubDerInf.ImagenPath(),
				DerechaInferiorUrl = pubDerInf.Url
			};
		}

		public void MapTorneos(PublicIndexVM vm, List<Torneo> torneos)
		{
			var anios = AniosEnLosQueHayTorneosPublicos(torneos);
			if (anios != null)
			{
				vm.AnioSeleccionadoId = $"Anio{anios.OrderByDescending(x => x.Descripcion()).Select(x => x.Descripcion()).First()}Link";

				foreach (var anio in anios)
				{
					var anioWebPublica = new AnioWebPublicaVM { Anio = Convert.ToInt32(anio.Descripcion()) };
					foreach (var torneo in torneos.Where(x => x.Anio == anio))
					{
						var torneoVM = MapTorneo(torneo);
						anioWebPublica.Torneos.Add(torneoVM);
					}
					vm.Anios.Add(anioWebPublica);
				}
			}
		}

		private static IList<Anio> AniosEnLosQueHayTorneosPublicos(IEnumerable<Torneo> torneos)
		{
			return torneos.Where(x => x.Publico).Select(x => x.Anio).OrderByDescending(x => x.Descripcion()).Distinct().ToList();
		}

		private static TorneoWebPublicaVM MapTorneo(Torneo torneo)
		{
			var result = new TorneoWebPublicaVM
			{
				Id = torneo.Id,
				TipoDesc = torneo.Tipo.Descripcion,
				Fomato = torneo.Tipo.Formato,
				Zonas = new List<ZonaVM>()
			};

			foreach (var zona in torneo.Zonas)
			{
				var zonaVM = new ZonaVM
				{
					Nombre = zona.Nombre,
					Id = zona.Id,
					Tipo = zona.Tipo,
					SancionesVisibles = torneo.SancionesHabilitadas
				};

				result.Zonas.Add(zonaVM);

				if (zonaVM.Tipo == ZonaTipo.Apertura)
					AgregarZonaAnual(zona, result);
			}			

			return result;
		}

		private static void AgregarZonaAnual(Zona zona, TorneoWebPublicaVM result)
		{
			var zonaVM = new ZonaVM
			{
				Nombre = zona.Nombre,
				Id = zona.Id,
				Tipo = ZonaTipo.Anual 
			};

			result.Zonas.Add(zonaVM);
		}

		public void MapDatosDeEquiposWebPublica(Zona zona, PublicIndexVM vm)
		{
			vm.DatosDeEquipos = new DatosDeEquiposVM($"Equipos de la zona {zona.Nombre}");
			var zonaHelper = new ZonaHelper(_context);

			foreach (var equipo in zonaHelper.EquiposDeLaZonaDatosParaLosDatosWebPublica(zona))
			{
				var renglon = new RenglonDatosEquipo
				{
					Equipo = equipo.Nombre,
					Escudo =  equipo.Club.EscudoPath(),
					Direccion = equipo.Club.Direccion,
					Localidad = equipo.Club.Localidad,					
					Techo = equipo.Club.TechoBoolToTechoEnum(),
					Delegado1 = equipo.Delegado1?.Descripcion,
					Delegado2 = equipo.Delegado2?.Descripcion,
					Telefono1 = equipo.Delegado1?.Telefono,
					Telefono2 = equipo.Delegado2?.Telefono
				};

				vm.DatosDeEquipos.Renglones.Add(renglon);
			}
		}

		public void MapPosicionesWebPublica(Zona zona, PublicIndexVM vm)
		{
			var builder = new TablaWebPublicaBuilder(_context);
			vm.Tablas = builder.Tablas(zona);
		}

		public void MapJornadasWebPublica(Zona zona, PublicIndexVM vm)
		{
			var resumenJornadasHelper = new ResumenDeJornadasBuilder(_context);
			var fechas = zona.Fechas.Where(x => x.Publicada).ToList();
			vm.Jornadas = resumenJornadasHelper.Tablas(zona, fechas);
		}

		public void MapFixtureWebPublica(Zona zona, PublicIndexVM vm)
		{
			vm.Fixture = MapFixtureWebPublica(zona);
		}

		private FixtureVM MapFixtureWebPublica(Zona zona)
		{
			var zonaVMM = new ZonaVMM(_context);
			var vm = new FixtureVM($"Fixture {zona.DescripcionCompleta()}")
			{
				ZonaId = zona.Id,
				PublicadoBool = zona.FixturePublicado
			};

			if (zona.FixturePublicado)
				zonaVMM.MapFixture(zona, vm);

			return vm;
		}

		public void MapPosicionesAnualesWebPublica(Zona zonaApertura, PublicIndexVM vm)
		{
			var builder = new TablaAnualWebPublicaBuilder(_context);
			vm.Tablas = builder.Tablas(zonaApertura);
			vm.Tablas.Titulo = $"TABLA ANUAL. Torneo: {zonaApertura.Torneo.Descripcion} - Zona: {zonaApertura.Nombre}";
		}

		public void MapGoleadoresWebPublica(Zona zona, PublicIndexVM vm)
		{
			var builder = new GoleadoresWebPublicaBuilder(_context);
			vm.Goleadores = builder.Tablas(zona);
		}

		public void MapSancionesWebPublica(Zona zona, PublicIndexVM vm)
		{
			vm.Sanciones = new SancionesWebPublicaVM($"Sanciones de la zona {zona.Nombre}");
			var sanciones = _context.Sanciones.Where(x => x.Jornada.Fecha.ZonaId == zona.Id && x.Visible).ToList();

			foreach (var sancion in sanciones)
			{
				var renglon = new RenglonSanciones
				{
					Sancion = sancion.Descripcion,
					Dia = DateTimeUtils.ConvertToString(sancion.Dia),
					Fecha = sancion.Jornada.Fecha.Numero.ToString(),
					Local = sancion.Jornada.NombreDelLocal(),
					Visitante = sancion.Jornada.NombreDelVisitante(),
					Categoria = sancion.Categoria.Nombre,
					FechasQueAdeuda = sancion.CantidadFechasQueAdeuda
				};

				vm.Sanciones.Renglones.Add(renglon);
			}
		}
	}
}