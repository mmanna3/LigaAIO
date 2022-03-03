using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LigaSoft.BusinessLogic;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class EquipoVMM : CommonVMM<Equipo, EquipoVM>
	{
		public EquipoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(EquipoVM vm, Equipo model)
		{
			model.Id = vm.Id;
			model.ClubId = vm.ClubId;
			model.Nombre = vm.Nombre;
			model.Delegado1Id = vm.Delegado1Id;
			model.Delegado2Id = vm.Delegado2Id;
			model.ValorDeLaCuota = vm.PrecioDeLaCuota;

			if (model.TorneoId != vm.TorneoId)
			{
				model.TorneoId = vm.TorneoId;
				model.ZonaId = null;
			}
		}

		public void MapCreate(EquipoVM vm, Equipo model)
		{
			model.ClubId = vm.ClubId;
			model.Nombre = vm.Nombre;
			model.TorneoId = vm.TorneoId;
			model.Delegado1Id = vm.Delegado1Id;
			model.Delegado2Id = vm.Delegado2Id;
			model.ValorDeLaCuota = vm.PrecioDeLaCuota;
		}

		public override IList<EquipoVM> MapForGrid(IList<Equipo> equipoList)
		{
			var listVM = new List<EquipoVM>();

			foreach (var equipo in equipoList)
				listVM.Add(MapForGrid(equipo));

			return listVM;
		}

		public EquipoVM MapForGrid(Equipo model)
		{			
			return new EquipoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Club = model.Club.Nombre,
				Torneo = model.Torneo.Descripcion,
				Zona = model.Zona?.Nombre
			};
		}

		public override EquipoVM MapForEditAndDetails(Equipo model)
		{
			return new EquipoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				ClubId = model.ClubId,
				Club = model.Club.Nombre,
				TorneoId = model.Torneo.Id,
				Torneo = model.Torneo.Descripcion,
				Zona = model.Zona?.Nombre,
				CantidadFichados = model.CantidadFichados(),
				Jugadores = MapForDisplayMultiline(model.JugadorEquipo),
				JugadoresIds = model.JugadorEquipo.Select(x => x.JugadorId),
				PrecioDeLaCuota = model.ValorDeLaCuota,
				Delegado1Id = model.Delegado1Id,
				Delegado1 = model.Delegado1?.Descripcion,
				Delegado2Id = model.Delegado2Id,
				Delegado2 = model.Delegado2?.Descripcion,
				CodigoAlfanumerico = GeneradorDeHash.GenerarAlfanumerico7Digitos(model.Id)
			};
		}

		public FicharJugadorExistenteVM MapJugadorExistente(Equipo equipo)
		{
			return new FicharJugadorExistenteVM
			{
				EquipoEnElQueLoEstoyFichandoId = equipo.Id,
				EquipoEnElQueLoEstoyFichandoNombre = equipo.Nombre
			};
		}

		public IEnumerable<string> MapForDisplayMultiline(IEnumerable<Equipo> models)
		{
			return models.Select(model => model.Descripcion() + " " + BotonSuspenderHabilitar(model.Id, model.BajaLogica)).ToList();
		}

		private static string BotonSuspenderHabilitar(int equipoId, bool estaDadoDeBaja)
		{
			string claseBootstrap;
			string label;
			if (estaDadoDeBaja)
			{
				claseBootstrap = "btn-danger";
				label = $"Equipo dado de baja. ¿Habilitar?";
			}
			else
			{
				claseBootstrap = "btn-success";
				label = $"Equipo habilitado. ¿Dar de baja?";
			}

			return $"<button onClick='return habilitarODarDeBaja({equipoId})' class='btn {claseBootstrap} btn-sm boton-suspenderhabilitar'>{label}</button>";
		}

		public IEnumerable<string> MapForDisplayMultiline(ICollection<JugadorEquipo> models)
		{
			return models
				.OrderBy(model => model.Jugador.Categoria())
				.Select(model => model.Descripcion())
				.ToList();
		}
	}
}