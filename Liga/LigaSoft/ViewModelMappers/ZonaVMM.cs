using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
	public class ZonaVMM : CommonVMM<Zona, ZonaVM>
	{
		private readonly IImagenesEscudosPersistence _imagenesEscudosPersistence;
		private readonly string _escudoDefault;

		public ZonaVMM(ApplicationDbContext context) : base(context)
		{
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
			_escudoDefault = context.ParametrizacionesGlobales.First().EscudoPorDefectoEnBase64;
		}

		public override void MapForCreateAndEdit(ZonaVM vm, Zona model)
		{
			model.Id = vm.Id;
			model.Nombre = vm.Nombre;
			model.Torneo = Context.Torneos.Find(vm.TorneoId);
			model.Tipo = vm.Tipo;
		}

		public override IList<ZonaVM> MapForGrid(IList<Zona> modelList)
		{
			var listVM = new List<ZonaVM>();

			foreach (var zona in modelList)
				listVM.Add(MapForEditAndDetails(zona));

			return listVM;
		}

		public override ZonaVM MapForEditAndDetails(Zona model)
		{
			var result = new ZonaVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				TorneoId = model.TorneoId,
				Torneo = model.Torneo.Descripcion,
				Tipo = model.Tipo,
				TipoDesc = model.Tipo.Descripcion()
			};
			MapTiposDeZonasDisponibles(model.Torneo.Tipo.Formato, result);

			return result;
		}

		public FixtureVM MapFixturePanelAdministrativo(Zona zona)
		{
			var vm = new FixtureVM($"Fixture {zona.DescripcionCompleta()}")
			{
				ZonaId = zona.Id,
				TorneoId = zona.TorneoId,
				PublicadoBool = zona.FixturePublicado
			};

			MapFixture(zona, vm);

			return vm;
		}

		public void MapFixture(Zona zona, FixtureVM vm)
		{
			var fechas = zona.Fechas.ToList();
			fechas.Sort((x, y) => x.DiaDeLaFecha.CompareTo(y.DiaDeLaFecha));

			foreach (var fecha in fechas)
			{
				var fechaVM = new FixtureFechaVM
				{
					Titulo = $"Fecha {fecha.Numero}",
					DiaDeLaFecha = $"{fecha.DiaDeLaFecha:d-M}",
					EquipoLibre = Context.Equipos.FirstOrDefault(x => x.Id == fecha.EquipoLibreId)?.Nombre,
				};

				foreach (var jornada in fecha.Jornadas)
				{
					var partido = new LocalVisitanteVM
					{
						Local = jornada.NombreDelLocal(),
						EscudoLocal =  EscudoLocal(jornada),
						Visitante = jornada.NombreDelVisitante(),
						EscudoVisitante = EscudoVisitante(jornada)
					};
					fechaVM.LocalVisitante.Add(partido);
				}
				vm.Fechas.Add(fechaVM);
			}
		}

		private string EscudoLocal(Jornada jornada)
		{
			if (jornada.Local != null)
				return _imagenesEscudosPersistence.Path(jornada.Local.Club.Id, _escudoDefault);

			var model = Context.ParametrizacionesGlobales.FirstOrDefault();
			return ImagenUtility.ProcesarImagenDeBDParaMostrarEnWeb(model.EscudoPorDefectoEnBase64);
		}

		private string EscudoVisitante(Jornada jornada)
		{
			if (jornada.Visitante != null)
				return _imagenesEscudosPersistence.Path(jornada.Visitante.Club.Id, _escudoDefault);

			var model = Context.ParametrizacionesGlobales.FirstOrDefault();
			return ImagenUtility.ProcesarImagenDeBDParaMostrarEnWeb(model.EscudoPorDefectoEnBase64);
		}

		public DatosDeEquiposVM MapDatosDeEquipos(Zona zona)
		{
			var vm = new DatosDeEquiposVM($"Clubes Zona {zona.Nombre}");
			vm.TorneoId = zona.TorneoId;
			vm.ZonaId = zona.Id;

			foreach (var equipo in zona.Equipos)
			{
				var renglon = new RenglonDatosEquipo
				{
					Equipo = equipo.Nombre,
					Direccion = equipo.Club.Direccion,
					Localidad = equipo.Club.Localidad,
					Techo = equipo.Club.TechoBoolToTechoEnum(),
					Delegado1 = equipo.Delegado1?.Descripcion,
					Delegado2 = equipo.Delegado2?.Descripcion,
					Telefono1 = equipo.Delegado1?.Telefono,
					Telefono2 = equipo.Delegado2?.Telefono
				};

				vm.Renglones.Add(renglon);
			}

			return vm;
		}

		public void MapTiposDeZonasDisponibles(TorneoFormato torneoFormato, ZonaVM vm)
		{
			vm.TiposDisponibles = new List<SelectListItem>();

			if (torneoFormato.Equals(TorneoFormato.AperturaClausura))
			{
				vm.TiposDisponibles.Add(new SelectListItem
				{
					Text = ZonaTipo.Apertura.Descripcion(),
					Value = ZonaTipo.Apertura.ToString()
				});
				vm.TiposDisponibles.Add(new SelectListItem
				{
					Text = ZonaTipo.Clausura.Descripcion(),
					Value = ZonaTipo.Clausura.ToString()
				});
			}
			else if (torneoFormato.Equals(TorneoFormato.Relampago))
			{
				vm.TiposDisponibles.Add(new SelectListItem
				{
					Text = ZonaTipo.Relampago.Descripcion(),
					Value = ZonaTipo.Relampago.ToString()
				});
			}
		}

		public PartidosPostergadosOSuspendidosVM MapPartidosPostergadosOSuspendidos(Zona zona)
		{
			var vm = new PartidosPostergadosOSuspendidosVM($"Partidos postergados o suspendidos. Zona: {zona.Nombre}");

			foreach (var fecha in zona.Fechas)
				foreach (var jornada in fecha.Jornadas)
				{
					var partidosSuspendidosOPostergadosDeLaJornada = jornada.PartidosSuspendidosOPostergados();

					foreach (var partido in partidosSuspendidosOPostergadosDeLaJornada)
					{
						var renglon = new RenglonPartidosPostergadosOSuspendidosVM()
						{
							Fecha = fecha.Numero.ToString(),
							Local = jornada.NombreDelLocal(),
							Visitante = jornada.NombreDelVisitante(),
							Categoria = partido.Categoria.Nombre,
							PostergadoOSuspendido = partido.GolesLocal,
						};

						vm.Renglones.Add(renglon);
					}
				}

			return vm;
		}
	}
}