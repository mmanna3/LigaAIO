using System.Collections.Generic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class TorneoTipoVMM : CommonVMM<TorneoTipo, TorneoTipoVM>
	{
		public TorneoTipoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(TorneoTipoVM viewModel, TorneoTipo model)
		{
			model.Descripcion = viewModel.Descripcion;
			model.ValidezDelCarnetEnAnios = viewModel.ValidezDelCarnetEnAnios;
			model.LoQueSeImprimeEnElCarnet = viewModel.LoQueSeImprimeEnElCarnet;
			model.Formato = viewModel.Formato;
		}

		public override IList<TorneoTipoVM> MapForGrid(IList<TorneoTipo> tiposList)
		{
			var listVM = new List<TorneoTipoVM>();

			foreach (var tipo in tiposList)
				listVM.Add(MapForEditAndDetails(tipo));

			return listVM;
		}

		public override TorneoTipoVM MapForEditAndDetails(TorneoTipo model)
		{
			var vm =  new TorneoTipoVM
			{
				Id = model.Id,
				Descripcion = model.Descripcion,
				LoQueSeImprimeEnElCarnet = model.LoQueSeImprimeEnElCarnet,
				ValidezDelCarnetEnAnios = model.ValidezDelCarnetEnAnios,
				Formato = model.Formato,
				FormatoDesc = model.Formato.Descripcion()
			};

			if (model.Formato.Equals(TorneoFormato.Relampago))
			{
				vm.ValidezDelCarnetEnAnios = 0;
				vm.LoQueSeImprimeEnElCarnet = "-";
			}

			return vm;
		}
	}
}