using System.Collections.Generic;
using LigaSoft.Models;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.ViewModelMappers
{
	public class ConceptoInsumoVMM : CommonVMM<ConceptoInsumo, ConceptoInsumoVM>
	{
		public ConceptoInsumoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(ConceptoInsumoVM viewModel, ConceptoInsumo model)
		{
			model.Descripcion = viewModel.Descripcion;
			model.Precio = viewModel.Precio;
		}

		public override IList<ConceptoInsumoVM> MapForGrid(IList<ConceptoInsumo> list)
		{
			var listVM = new List<ConceptoInsumoVM>();

			foreach (var tipo in list)
				listVM.Add(MapForEditAndDetails(tipo));

			return listVM;
		}

		public override ConceptoInsumoVM MapForEditAndDetails(ConceptoInsumo model)
		{
			return new ConceptoInsumoVM
			{
				Id = model.Id,
				Descripcion = model.Descripcion,
				Precio = model.Precio,
				Stock = model.Stock,
				Visible = model.Visible.ToSiNoString()
			};
		}

		public ConceptoInsumoAgregarStockVM MapForAgregarStock(ConceptoInsumo model)
		{
			return new ConceptoInsumoAgregarStockVM
			{
				Id = model.Id,
				Descripcion = model.Descripcion,
				Precio = model.Precio,
				Stock = model.Stock
			};
		}
	}
}