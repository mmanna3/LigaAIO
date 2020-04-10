using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
    public class ParametroGlobalVMM : CommonVMM<ParametroGlobal, ParametroGlobalVM>
	{
		public ParametroGlobalVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(ParametroGlobalVM vm, ParametroGlobal model)
		{
			model.ValorPorDefectoEnPesosDelConceptoFichaje = vm.ValorPorDefectoEnPesosDelConceptoFichaje;

			if (vm.EscudoNuevo != null)
				model.EscudoPorDefectoEnBase64 = ImagenUtility.StreamToBase64(vm.EscudoNuevo.InputStream);
		}

		public override ParametroGlobalVM MapForEditAndDetails(ParametroGlobal model)
		{
			return new ParametroGlobalVM
			{
				ValorPorDefectoEnPesosDelConceptoFichaje = model.ValorPorDefectoEnPesosDelConceptoFichaje,
				EscudoActual = ImagenUtility.ProcesarImagenDeBDParaMostrarEnWeb(model.EscudoPorDefectoEnBase64)
			};
		}
	}
}