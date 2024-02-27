using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
    public class JugadorSancionadoDePorVidaVMM : CommonVMM<JugadorSancionadoDePorVida, JugadorSancionadoDePorVidaVM>
	{

		public JugadorSancionadoDePorVidaVMM(ApplicationDbContext context) : base(context)
		{
			
		}

		public override void MapForCreateAndEdit(JugadorSancionadoDePorVidaVM vm, JugadorSancionadoDePorVida model)
		{
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.Apellido = vm.Apellido;
			model.Motivo = vm.Motivo;
		}

		public override JugadorSancionadoDePorVidaVM MapForEditAndDetails(JugadorSancionadoDePorVida model)
		{
			return new JugadorSancionadoDePorVidaVM
			{
				Id = model.Id,
				DNI = model.DNI,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				Motivo = model.Motivo,
			};
		}
	}
}