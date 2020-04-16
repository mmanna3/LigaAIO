using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class ConceptoInsumoController : ABMController<ConceptoInsumo, ConceptoInsumoVM, ConceptoInsumoVMM>
	{
		public int PrecioUnitario(int conceptoId)
		{
			var model = Context.ConceptosInsumo.SingleOrDefault(x => x.Id == conceptoId);

			return model?.Precio ?? 0;
		}

		public ActionResult AgregarStock(int id)
		{
			var conceptoInsumo = Context.ConceptosInsumo.Find(id);
			var vm = VMM.MapForAgregarStock(conceptoInsumo);
			return View(vm);
		}

		[HttpPost]
		public ActionResult AgregarStock(ConceptoInsumoAgregarStockVM vm)
		{
			var conceptoInsumo = Context.ConceptosInsumo.Single(x => x.Id == vm.Id);
			conceptoInsumo.IncrementarStock(vm.StockASumar);

			Context.SaveChanges();

			return RedirectToAction("Index");
		}

		public JsonResult Autocomplete(string term = "")
		{
			var result = Context.ConceptosInsumo
				.ToList()
				.Select(c => new IdDescripcionVM
				{
					Id = c.Id,
					Descripcion = $"{c.Descripcion} - Stock: {c.Stock}"
				})
				.ToList();

			return Json(result, JsonRequestBehavior.AllowGet);
		}
	}
}