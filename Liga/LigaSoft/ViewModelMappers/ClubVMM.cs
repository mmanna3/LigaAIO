using System.Collections.Generic;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class ClubVMM : CommonVMM<Club, ClubVM>
	{
		public ClubVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(ClubVM vm, Club model)
		{
			model.Id = vm.Id;
			model.Direccion = vm.Direccion;
			model.Localidad = vm.Localidad;
			model.Nombre = vm.Nombre;
			model.Techo = vm.TechoEnumToNullableBool();
		}

		public override IList<ClubVM> MapForGrid(IList<Club> clubList)
		{
			var listVM = new List<ClubVM>();

			foreach (var club in clubList)
				listVM.Add(MapForGrid(club));

			return listVM;
		}

		public ClubVM MapForGrid(Club model)
		{
			return new ClubVM
			{
				Id = model.Id,
				Direccion = model.Direccion,
				Localidad = model.Localidad,
				Nombre = model.Nombre,
				Techo = model.TechoBoolToTechoEnum()
			};
		}

		public override ClubVM MapForEditAndDetails(Club model)
		{
			var equipoVMM = new EquipoVMM(Context);

			var vm = new ClubVM
			{
				Id = model.Id,
				Direccion = model.Direccion,
				Localidad = model.Localidad,
				Nombre = model.Nombre,
				Techo = model.TechoBoolToTechoEnum(),
				Cuota = $"${model.Cuota()}",
				Escudo = model.EscudoPath()
			};

			MapConceptoTotales(model, vm);

			if (model.Equipos != null)
				vm.Equipos = equipoVMM.MapForDisplayMultiline(model.Equipos);

			return vm;
		}

		private static void MapConceptoTotales(Club model, ClubVM vm)
		{
			vm.ConceptoTotales = new ConceptoTotales
			{
				DeudaCuotas = $"${model.DeudaCuotas()}",
				DeudaFichajes = $"${model.DeudaFichajes()}",
				DeudaInsumos = $"${model.DeudaInsumos()}",
				DeudaLibres = $"${model.DeudaLibre()}",
				DeudaTotal = $"${model.DeudaTotal()}"
			};
		}

		public IList<DelegadoVM> MapForDelegadosGrid(List<Delegado> list)
		{
			var listVM = new List<DelegadoVM>();

			foreach (var delegado in list)
				listVM.Add(MapForGrid(delegado));

			return listVM;
		}

		private static DelegadoVM MapForGrid(Delegado model)
		{
			return new DelegadoVM
			{
				Id = model.Id,
				Descripcion = model.Descripcion,
				Telefono = model.Telefono
			};
		}
	}
}