using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
    public class PublicidadVMM : CommonVMM<Publicidad, PublicidadVM>
	{
		private ImagenesPublicidadDiskPersistence _imagenes;

		public PublicidadVMM(ApplicationDbContext context) : base(context)
		{
			_imagenes = new ImagenesPublicidadDiskPersistence(new AppPathsWebApp());
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
				Posicion = model.Posicion.Descripcion(),
				ImagenActualUrl = _imagenes.Path(model.Id)

			};
		}
	}
}