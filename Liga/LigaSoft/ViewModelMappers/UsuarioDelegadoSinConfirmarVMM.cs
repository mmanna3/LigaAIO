using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class UsuarioDelegadoSinConfirmarVMM : CommonVMM<UsuarioDelegadoSinConfirmar, UsuarioDelegadoSinConfirmarVM>
	{
		public UsuarioDelegadoSinConfirmarVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(UsuarioDelegadoSinConfirmarVM vm, UsuarioDelegadoSinConfirmar model)
		{
			throw new System.NotImplementedException();
		}

		public override UsuarioDelegadoSinConfirmarVM MapForEditAndDetails(UsuarioDelegadoSinConfirmar model)
		{
			throw new System.NotImplementedException();
		}
	}
}