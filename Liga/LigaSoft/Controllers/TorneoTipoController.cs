using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;
using LigaSoft.Models.Attributes.GPRPattern;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class TorneoTipoController : ABMController<TorneoTipo, TorneoTipoVM, TorneoTipoVMM>
    {
		[HttpPost, ExportModelStateToTempData]
		public override ActionResult Edit(TorneoTipoVM viewModel)
		{
			var model = Context.TorneoTipos.Find(viewModel.Id);

			model.ValorDelFichajeEnPesos = viewModel.ValorDelFichajeEnPesos;

			Context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}