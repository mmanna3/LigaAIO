using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class SancionController : ABMControllerWithParent<Sancion, SancionVM, SancionVMM, Zona, ZonaVM, ZonaVMM>
	{
		public SancionController() : base("Zona", "Jornada.Fecha.ZonaId")
		{
		}

		[ImportModelStateFromTempData]
		public override ActionResult Create(int parentId)
		{
			var vm = VMM.InitSancionVM(parentId);

			return View(vm);
		}

		[ImportModelStateFromTempData]
		public override ActionResult Edit(int id)
		{
			var model = Context.Sanciones.Find(id);

			var vm = VMM.MapForEdit(model);

			return View(vm);
		}

		protected override int GetParentId(SancionVM vm)
		{
			return vm.ZonaId;
		}

		public JsonResult JornadasDeLaFecha(int fechaId)
		{
			var jornadas = Context.Fechas.Single(x => x.Id == fechaId).Jornadas
				.ToList()
				.Select(x => new TextValueItem {Text = $"{x.Descripcion()}", Value = x.Id.ToString()})
				.ToList();

			return Json(jornadas, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Eliminar(int id)
		{
			var model = Context.Sanciones.Find(id);

			Context.Sanciones.Remove(model);

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult MostrarOcultar(int id)
		{
			var model = Context.Sanciones.Find(id);

			model.Visible = !model.Visible;

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult RestarFechaAdeudada(int id)
		{
			var model = Context.Sanciones.Find(id);

			if (model.CantidadFechasQueAdeuda > 0)
			{
				model.CantidadFechasQueAdeuda--;
				Context.SaveChanges();
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}