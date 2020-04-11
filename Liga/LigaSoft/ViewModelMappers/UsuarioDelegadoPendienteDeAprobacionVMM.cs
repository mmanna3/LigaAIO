using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class UsuarioDelegadoPendienteDeAprobacionVMM : CommonVMM<UsuarioDelegadoPendienteDeAprobacion, UsuarioDelegadoPendienteDeAprobacionVM>
	{
		public UsuarioDelegadoPendienteDeAprobacionVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(UsuarioDelegadoPendienteDeAprobacionVM vm, UsuarioDelegadoPendienteDeAprobacion model)
		{
			model.Email = vm.Email;
			model.Password = vm.Password;
			model.ClubId = vm.ClubId;
		}

		public override UsuarioDelegadoPendienteDeAprobacionVM MapForEditAndDetails(UsuarioDelegadoPendienteDeAprobacion model)
		{
			return new UsuarioDelegadoPendienteDeAprobacionVM
			{
				Email = model.Email,
				ClubId = model.ClubId,
				Club = Context.Clubs.Single(x => x.Id == model.ClubId).Nombre
			};
		}
	}
}