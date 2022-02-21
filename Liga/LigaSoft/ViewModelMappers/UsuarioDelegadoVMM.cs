using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class UsuarioDelegadoVMM : CommonVMM<UsuarioDelegado, UsuarioDelegadoVM>
	{
		public UsuarioDelegadoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(UsuarioDelegadoVM vm, UsuarioDelegado model)
		{
			model.Usuario = vm.Usuario;
			model.Password = vm.Password;
			model.ClubId = vm.ClubId;
			model.Nombre = vm.Nombre;
			model.Apellido = vm.Apellido;
		}

		public override UsuarioDelegadoVM MapForEditAndDetails(UsuarioDelegado model)
		{
			return new UsuarioDelegadoVM
			{
				Id = model.Id,
				Usuario = model.Usuario,
				ClubId = model.ClubId,
				Club = Context.Clubs.Single(x => x.Id == model.ClubId).Nombre,
				Apellido = model.Apellido,
				Nombre = model.Nombre
			};
		}
	}
}