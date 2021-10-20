using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class JornadaController : ABMControllerWithParent<Jornada, JornadaVM, JornadaVMM, Fecha, FechaVM, FechaVMM>
    {
	    public JornadaController() : base("Fecha","FechaId")
	    {
	    }

	    [ImportModelStateFromTempData]
		public ActionResult CargarPartidos(int id)
	    {
		    var vm = VMM.MapForCargarPartidos(id);
		    return View(vm);
	    }

		[ExportModelStateToTempData, HttpPost]
		public ActionResult CargarPartidos(JornadaVM vm)
	    {
		    if (!ModelState.IsValid || UnoSuspendidoYElOtroNo(vm))
			    return RedirectToAction("CargarPartidos", new {id = vm.Id});

		    var model = Context.Jornadas.Find(vm.Id);

		    VMM.MapForCargarPartidos(vm, model);

			Context.SaveChanges();

			return RedirectTo("Index", vm.FechaId);
	    }

		[ImportModelStateFromTempData]
	    public ActionResult VerificarResultados(int id)
	    {
		    var jornada = Context.Jornadas.Find(id);
		    var vm = VMM.MapForEditAndDetails(jornada);
		    return View(vm);
	    }

		[ExportModelStateToTempData, HttpPost]
	    public ActionResult VerificarResultados(JornadaVM vm)
	    {
		    var model = Context.Jornadas.Find(vm.Id);

		    VMM.MapForVerificarResultados(vm, model);

		    Context.SaveChanges();

		    return RedirectTo("Index", vm.FechaId);
	    }

		private bool UnoSuspendidoYElOtroNo(JornadaVM vm)
	    {
		    foreach (var partido in vm.Partidos)
		    {
			    if (partido.GolesLocal.ToUpper() == "S" && partido.GolesVisitante.ToUpper() != "S" ||
			        (partido.GolesVisitante.ToUpper() == "S" && partido.GolesLocal.ToUpper() != "S"))
			    {
				    ModelState.AddModelError("", "Para que el partido se suspenda, los resultados de los dos equipos deben ser S.");
				    return true;
			    }
			    if (partido.GolesLocal.ToUpper() == "P" && partido.GolesVisitante.ToUpper() != "P" ||
			        (partido.GolesVisitante.ToUpper() == "P" && partido.GolesLocal.ToUpper() != "P"))
			    {
				    ModelState.AddModelError("", "Para que el partido se posterga, los resultados de los dos equipos deben ser P.");
				    return true;
			    }
				if (partido.GolesLocal.ToUpper() == "AR" && partido.GolesVisitante.ToUpper() != "AR" ||
					(partido.GolesVisitante.ToUpper() == "AR" && partido.GolesLocal.ToUpper() != "AR"))
				{
					ModelState.AddModelError("", "Para que el partido quede a resolver, los resultados de los dos equipos deben ser AR.");
					return true;
				}
			}
		    return false;
	    }
    }
}