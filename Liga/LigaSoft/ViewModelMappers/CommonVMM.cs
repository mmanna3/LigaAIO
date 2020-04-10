using System.Collections.Generic;
using LigaSoft.Models;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public abstract class CommonVMM<TModel, TViewModel>
		where TViewModel : ViewModelConId
	{
		protected ApplicationDbContext Context;

		protected CommonVMM(ApplicationDbContext context)
		{
			Context = context;
		}

		public abstract void MapForCreateAndEdit(TViewModel vm, TModel model);
		public abstract TViewModel MapForEditAndDetails(TModel model);

		public virtual void MapForCreate(TViewModel vm, TModel model)
		{
			vm.Id = 0;
			MapForCreateAndEdit(vm, model);
		}
		public virtual void MapForEdit(TViewModel vm, TModel model)
		{
			MapForCreateAndEdit(vm, model);
		}

		public virtual TViewModel MapForEdit(TModel model)
		{
			return MapForEditAndDetails(model);
		}
		public virtual TViewModel MapForDetails(TModel model)
		{
			return MapForEditAndDetails(model);
		}

        public virtual IList<TViewModel> MapForGrid(IList<TModel> modelList)
        {
            var listVM = new List<TViewModel>();

            foreach (var model in modelList)
                listVM.Add(MapForEditAndDetails(model));

            return listVM;
        }
    }
}