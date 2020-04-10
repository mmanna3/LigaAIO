using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class NoticiaController : CommonController<Noticia, NoticiaVM, NoticiaVMM>
	{
		[HttpPost]
		public ActionResult OcultarMostrar(int id)
		{
			var model = Context.Noticias.Find(id);

			model.Visible = !model.Visible;

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Eliminar(int id)
		{
			var model = Context.Noticias.Find(id);

			Context.Noticias.Remove(model);

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, ExportModelStateToTempData, ValidateInput(false)]
		public override ActionResult Create(NoticiaVM viewModel)
		{
			if (!ModelState.IsValid)
				return RedirectToAction("Create");

			var model = new Noticia();

			VMM.MapForCreate(viewModel, model);

			Context.Noticias.Add(model);

			Context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpPost, ExportModelStateToTempData, ValidateInput(false)]
		public override ActionResult Edit(NoticiaVM viewModel)
		{
			if (!ModelState.IsValid)
				return RedirectToAction("Edit");

			var model = Context.Noticias.Find(viewModel.Id);

			VMM.MapForEdit(viewModel, model);

			Context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}