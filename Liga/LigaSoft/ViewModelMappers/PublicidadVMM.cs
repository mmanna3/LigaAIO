using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
    public class PublicidadVMM : CommonVMM<Publicidad, PublicidadVM>
	{
		public PublicidadVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(PublicidadVM vm, Publicidad model)
		{
			model.Titulo = vm.Titulo;
			model.Url = vm.Url;			
		}

		public override PublicidadVM MapForEditAndDetails(Publicidad model)
		{
			return new PublicidadVM
			{
				Id = model.Id,
				Titulo = model.Titulo,
				Url = model.Url,
				Posicion = model.Posicion.Descripcion()
			};
		}
	}
}