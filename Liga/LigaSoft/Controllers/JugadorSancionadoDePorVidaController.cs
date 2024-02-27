using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class JugadorSancionadoDePorVidaController : ABMController<JugadorSancionadoDePorVida, JugadorSancionadoDePorVidaVM, JugadorSancionadoDePorVidaVMM>
	{
		[HttpPost]
		public ActionResult Eliminar(int id)
		{
			var model = Context.JugadoresSancionadosDePorVida.Find(id);

			Context.JugadoresSancionadosDePorVida.Remove(model);

			Context.SaveChanges();

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}