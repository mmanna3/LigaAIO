using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Script.Serialization;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using Newtonsoft.Json;	

namespace LigaSoft.ViewModelMappers
{
	public class PartidoVMM : CommonVMM<Partido, PartidoVM>
	{
		public PartidoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(PartidoVM vm, Partido model)
		{
			model.CategoriaId = vm.CategoriaId;
			model.GolesLocal = vm.GolesLocal.ToUpper();
			model.GolesVisitante = vm.GolesVisitante.ToUpper();
		}

		public override IList<PartidoVM> MapForGrid(IList<Partido> list)
		{
			var listVM = new List<PartidoVM>();

			foreach (var item in list)
				listVM.Add(MapForEditAndDetails(item));

			return listVM;
		}

		public void MapForCargarGoleadores(CargarGoleadoresVM vm, Partido model)
		{
			model.Goleadores = new List<Goleador>();

			if (vm.GoleadoresDelLocal != null)
				for (var i = 0; i < vm.GoleadoresDelLocal.Length; i++)
				{
					var jug = Context.Jugadores.Find(vm.GoleadoresDelLocal[i]);
					var goleador = new Goleador
					{
						JugadorId = jug.Id,
						EquipoId = model.Jornada.LocalIdInt(),
						Cantidad = vm.CantidadDeGolesGoleadorLocal[i],
						PartidoId = model.Id					
					};
					model.Goleadores.Add(goleador);
				}

			if (vm.GoleadoresDelVisitante != null)
				for (var i = 0; i < vm.GoleadoresDelVisitante.Length; i++)
				{
					var jug = Context.Jugadores.Find(vm.GoleadoresDelVisitante[i]);
					var goleador = new Goleador
					{
						JugadorId = jug.Id,
						EquipoId = model.Jornada.VisitanteIdInt(),
						Cantidad = vm.CantidadDeGolesGoleadorVisitante[i],
						PartidoId = model.Id
					};
					model.Goleadores.Add(goleador);
				}
		}

		public override PartidoVM MapForEditAndDetails(Partido model)
		{
			return new PartidoVM
			{
				Id = model.Id,
				FechaId = model.Jornada.FechaId,
				JornadaId = model.JornadaId,
				Categoria = model.Categoria.Nombre,
				Local = model.Jornada.NombreDelLocal(),
				Visitante = model.Jornada.NombreDelVisitante(),
				GolesLocal = model.GolesLocal,
				GolesVisitante = model.GolesVisitante,
				Goleadores = FormatearGoleadores(model.Goleadores)
			};
		}

		private static IEnumerable<string> FormatearGoleadores(ICollection<Goleador> goleadores)
		{
			var result = new List<string>();

			foreach (var goleador in goleadores)
				result.Add($"EQUIPO: {goleador.Equipo.Nombre} --- {goleador.Jugador.Apellido}, {goleador.Jugador.Nombre} --- CANTIDAD: {goleador.Cantidad}"); 

			return result;
		}

		public CargarGoleadoresVM MapForCargarGoleadores(IList<Goleador> goleadores, Partido partido)
		{
			var jugadoresDelLocal = partido.Jornada.Local.JugadorEquipo
				.OrderBy(x => x.Jugador.Categoria())
				.Select(x => new IdDescripcionVM
				{
					Id = Convert.ToInt32(x.JugadorId),
					Descripcion = x.Jugador.Descripcion()
				})
				.ToList();

			var jugadoresDelVisitante = partido.Jornada.Visitante?.JugadorEquipo
				.OrderBy(x => x.Jugador.Categoria())
				.Select(x => new IdDescripcionVM
				{
					Id = Convert.ToInt32(x.JugadorId),
					Descripcion = x.Jugador.Descripcion()
				})
				.ToList();			

			var vm = new CargarGoleadoresVM
			{
				JornadaId = partido.JornadaId,
				PartidoId = partido.Id,
				CategoriaId = partido.Categoria.Id,
				Titulo = $"Cat: {partido.Categoria.Nombre} - {partido.Jornada.NombreDelLocal()}: {partido.GolesLocal} {partido.Jornada.NombreDelVisitante()}: {partido.GolesVisitante}",
				TotalDeGolesDelLocal = ParseGoles(partido.GolesLocal),
				TotalDeGolesDelVisitante = ParseGoles(partido.GolesVisitante),
				TodosLosJugadoresDelLocal = JsonConvert.SerializeObject(jugadoresDelLocal),
				TodosLosJugadoresDelVisitante = JsonConvert.SerializeObject(jugadoresDelVisitante),
				EquipoLocalNombre = partido.Jornada.NombreDelLocal(),
				EquipoVisitanteNombre = partido.Jornada.NombreDelVisitante()				
			};

			MapGoleadores(goleadores, vm);

			return vm;
		}

		private static void MapGoleadores(IList<Goleador> model, CargarGoleadoresVM vm)
		{
			var goleadoresLocal = model.Where(x => x.EquipoId == x.Partido.Jornada.LocalId).ToList();
			var goleadoresVisitante = model.Where(x => x.EquipoId == x.Partido.Jornada.VisitanteId).ToList();

			vm.GoleadoresDelLocal = new int[goleadoresLocal.Count];
			vm.CantidadDeGolesGoleadorLocal = new int[goleadoresLocal.Count];
			for (var i = 0; i < goleadoresLocal.Count; i++)
			{
				vm.GoleadoresDelLocal[i] = goleadoresLocal[i].JugadorId;
				vm.CantidadDeGolesGoleadorLocal[i] = goleadoresLocal[i].Cantidad;
			}

			vm.GoleadoresDelVisitante = new int[goleadoresVisitante.Count];
			vm.CantidadDeGolesGoleadorVisitante = new int[goleadoresVisitante.Count];
			for (var i = 0; i < goleadoresVisitante.Count; i++)
			{
				vm.GoleadoresDelVisitante[i] = goleadoresVisitante[i].JugadorId;
				vm.CantidadDeGolesGoleadorVisitante[i] = goleadoresVisitante[i].Cantidad;
			}
		}

		private static int ParseGoles(string goles)
		{
			return int.TryParse(goles, out var result) ? result : 0;
		}
	}
}