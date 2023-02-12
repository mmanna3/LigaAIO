using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
    public class ParametroGlobalVMM : CommonVMM<ParametroGlobal, ParametroGlobalVM>
	{
		private readonly IImagenesEscudosPersistence _imagenesEscudosPersistence;

		public ParametroGlobalVMM(ApplicationDbContext context) : base(context)
		{
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		}

		public override void MapForCreateAndEdit(ParametroGlobalVM vm, ParametroGlobal model)
		{
			if (vm.EscudoNuevo != null)
			{
				model.EscudoPorDefectoEnBase64 = ImagenUtility.StreamToBase64(vm.EscudoNuevo.InputStream);
				_imagenesEscudosPersistence.GuardarEscudoDefault(model.EscudoPorDefectoEnBase64);
			}			
		}

		public override ParametroGlobalVM MapForEditAndDetails(ParametroGlobal model)
		{
			return new ParametroGlobalVM
			{
				EscudoActual = _imagenesEscudosPersistence.PathRelativoDelEscudoDefault()
		};
		}
	}
}