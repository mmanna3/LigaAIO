using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
	public class JugadorVMM : CommonVMM<Jugador, JugadorBaseVM>
	{
		private static ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;		

		public JugadorVMM(ApplicationDbContext context) : base(context)
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}

		public override void MapForCreateAndEdit(JugadorBaseVM vm, Jugador model)
		{
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.FechaNacimiento = DateTimeUtils.ConvertToDateTime(vm.FechaNacimiento);
			model.Apellido = vm.Apellido;
			model.CarnetImpreso = vm.CarnetImpresoBool;
		}

		public Image Base64ToImage(string base64String)
		{
			var imageBytes = Convert.FromBase64String(base64String);
			using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
			{
				var image = Image.FromStream(ms, true);
				return image;
			}
		}

		public override void MapForEdit(JugadorBaseVM vm, Jugador model)
		{
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.Apellido = vm.Apellido;
			model.FechaNacimiento = DateTimeUtils.ConvertToDateTime(vm.FechaNacimiento);
		}

		public JugadorEquipo MapCreate(FicharNuevoJugadorVM vm, Jugador model)
		{
			base.MapForCreate(vm, model);

			return new JugadorEquipo
			{
				Equipo = Context.Equipos.Find(vm.EquipoId),
				Jugador = model,
				FechaFichaje = DateTime.Today
			};
		}

		public override IList<JugadorBaseVM> MapForGrid(IList<Jugador> jugadorList)
		{
			var listVM = new List<JugadorBaseVM>();

			foreach (var jugador in jugadorList)
				listVM.Add(MapJugadorBase(jugador));

			return listVM;
		}

		public IList<JugadorConFechaFichajeVM> MapForImprimirJugadores(IList<JugadorEquipo> jugadoresEquipos)
		{
			var listVM = new List<JugadorConFechaFichajeVM>();

			foreach (var model in jugadoresEquipos)
			{
				var vm = new JugadorConFechaFichajeVM
				{
					Id = model.Jugador.Id,
					Nombre = model.Jugador.Nombre,
					Apellido = model.Jugador.Apellido,
					DNI = model.Jugador.DNI,
					FechaNacimiento = DateTimeUtils.ConvertToString(model.Jugador.FechaNacimiento),
					FechaFichaje = DateTimeUtils.ConvertToStringDdMmYy(model.FechaFichaje),
				};

				listVM.Add(vm);
			}
				
			return listVM;
		}

		public override JugadorBaseVM MapForEditAndDetails(Jugador model)
		{
			var vm = MapJugadorBase(model);

			vm.Equipos = new List<string>();

			var equiposListConBotonImprimir = model.JugadorEquipo.Select(x => x.Equipo.Descripcion() + " - FICHADO: " + DateTimeUtils.ConvertToStringDdMmYy(x.FechaFichaje) + " " + BotonImprimir(x.JugadorId, x.EquipoId));

			vm.Equipos.AddRange(equiposListConBotonImprimir);

			return vm;
		}

		private static string BotonImprimir(int jugadorId, int equipoId)
		{
			return $"<a href='PrntCarnet:/{equipoId}/{jugadorId}' class='btn btn-primary btn-sm boton-imprimir'>Imprimir carnet</a>";
		}

		public JugadorCarnetVM MapJugadorParaCarnet(Jugador model, Equipo equipo)
		{
			var jugadorEquipo = Context.JugadorEquipos.Single(x => x.EquipoId == equipo.Id && x.JugadorId == model.Id);

			return new JugadorCarnetVM
			{
				Nombre = model.Nombre.ToUpper(),
				Apellido = model.Apellido.ToUpper(),
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				Equipo = equipo.Nombre.ToUpper(),
				Categoria = Categoria(model),				
				FotoBase64 = _imagenesJugadoresDiskPersistence.GetFotoEnBase64(model.DNI),
				FechaVencimiento = FechaDeVencimientoDelCarnet(jugadorEquipo),
				TipoLiga = equipo.Torneo.Tipo.LoQueSeImprimeEnElCarnet.ToUpper()
			};
		}

		private static string Categoria(Jugador model)
		{
			return model.FechaNacimiento.ToString("yyyy");
		}

		private static string FechaDeVencimientoDelCarnet(JugadorEquipo jugadorEquipo)
		{
			var anio = jugadorEquipo.FechaFichaje.Year + jugadorEquipo.Equipo.Torneo.Tipo.ValidezDelCarnetEnAnios - 1;
			return DateTimeUtils.ConvertToString(new DateTime(anio, 12, 31));
		}

		private static JugadorBaseVM MapJugadorBase(Jugador model)
		{
			return new JugadorBaseVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				CarnetImpresoBool = model.CarnetImpreso,
				CarnetImpreso = model.CarnetImpreso.ToSiNoString(),
				Foto = _imagenesJugadoresDiskPersistence.Path(model.DNI)
			};
		}

		public EditFotoJugadorDesdeArchivoVM MapForEditFotoJugadorDesdeArchivo(Jugador model)
		{
			return new EditFotoJugadorDesdeArchivoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento)
			};
		}
	}
}