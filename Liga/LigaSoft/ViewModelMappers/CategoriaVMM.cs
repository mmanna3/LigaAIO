using System.Collections.Generic;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class CategoriaVMM : CommonVMM<Categoria, CategoriaVM>
	{
		public CategoriaVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(CategoriaVM vm, Categoria model)
		{
			model.Id = vm.Id;
			model.Nombre = vm.Nombre;
			model.Orden = vm.Orden;
			model.Torneo = Context.Torneos.Find(vm.TorneoId);
			model.AnioNacimientoDesde = vm.AnioNacimientoDesde;
			model.AnioNacimientoHasta = vm.AnioNacimientoHasta;
		}

		public override IList<CategoriaVM> MapForGrid(IList<Categoria> modelList)
		{
			var listVM = new List<CategoriaVM>();

			foreach (var cat in modelList)
				listVM.Add(MapForEditAndDetails(cat));

			return listVM;
		}

		public override CategoriaVM MapForEditAndDetails(Categoria model)
		{
			return new CategoriaVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Orden = model.Orden,
				TorneoId = model.TorneoId,
				Torneo = model.Torneo.Descripcion,
				AnioNacimientoDesde = model.AnioNacimientoDesde,
				AnioNacimientoHasta = model.AnioNacimientoHasta
			};
		}
	}
}