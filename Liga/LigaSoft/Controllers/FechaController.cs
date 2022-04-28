using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class FechaController : ABMControllerWithParent<Fecha, FechaVM, FechaVMM, Zona, ZonaVM, ZonaVMM>
    {
	    public FechaController() : base("Zona","ZonaId")
	    {
	    }

	    [ImportModelStateFromTempData]
	    public override ActionResult Create(int parentId)
	    {
		    var vm = FechaVMParaCreacion(parentId);

			return View(vm);
	    }

	    [ImportModelStateFromTempData]
	    public override ActionResult Edit(int id)
	    {
			var fecha = Context.Fechas.Single(x => x.Id == id);		    
			var vm = FechaVMParaEdicion(fecha);
		    vm.DiaDeLaFecha = DateTimeUtils.ConvertToString(fecha.DiaDeLaFecha);

		    return View(vm);
	    }

	    private EditarFechaVM FechaVMParaEdicion(Fecha fecha)
	    {
			var equiposDeLaZona = EquiposDeLaZona(fecha.ZonaId);
		    var zona = Context.Zonas.Single(x => x.Id == fecha.ZonaId);

		    var vm = new EditarFechaVM
			{
			    ZonaId = fecha.ZonaId,
			    EquiposDeLaZonaJson = new JavaScriptSerializer().Serialize(equiposDeLaZona),
			    EquiposDeLaZona = string.Join(" - ", equiposDeLaZona.Select(x => x.Descripcion)),
			    Titulo = $"Editar fecha {fecha.Numero} de {zona.DescripcionCompleta()}",
			    CantidadDeJornadas = fecha.Jornadas.Count,			    
				LocalesDefault = new List<IdDescripcionVM>(),
				VisitantesDefault = new List<IdDescripcionVM>(),
			};

		    foreach (var jornada in fecha.Jornadas)
		    {
			    vm.LocalesDefault.Add(new IdDescripcionVM{Id = jornada.LocalIdInt(), Descripcion = jornada.NombreDelLocal()});
			    vm.VisitantesDefault.Add(new IdDescripcionVM{Id = jornada.VisitanteIdInt(), Descripcion = jornada.NombreDelVisitante()});
		    }

		    return vm;
	    }

		private FechaVM FechaVMParaCreacion(int zonaId)
	    {
		    var equiposDeLaZona = EquiposDeLaZona(zonaId);
		    var zona = Context.Zonas.Single(x => x.Id == zonaId);

		    var vm = new FechaVM
		    {
			    ZonaId = zonaId,
			    EquiposDeLaZonaJson = new JavaScriptSerializer().Serialize(equiposDeLaZona),
			    EquiposDeLaZona = string.Join(" - ", equiposDeLaZona.Select(x => x.Descripcion)),
			    Titulo = $"Crear fecha en {zona.DescripcionCompleta()}",
			    CantidadDeJornadas = equiposDeLaZona.Count
		    };

		    return vm;
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Create(FechaVM vm)
	    {
		    if (!ModelState.IsValid || HayInconsistencia(vm))
			    return RedirectToAction("Create", new {parentId = vm.ZonaId});

			var model = new Fecha();

			VMM.MapForCreate(vm, model);

		    Context.Fechas.Add(model);

			Context.SaveChanges();

		    return RedirectTo("Index", vm.ZonaId);
	    }

	    [HttpPost, ExportModelStateToTempData]
	    public override ActionResult Edit(FechaVM vm)
	    {
			vm.DepurarJornadas();
			if (!ModelState.IsValid || HayInconsistencia(vm))
			    return RedirectToAction("Edit", new { id = vm.Id });

		    var model = Context.Fechas.Single(x => x.Id == vm.Id);

		    VMM.MapForEdit(vm, model);

		    Context.SaveChanges();

		    return RedirectTo("Index", vm.ZonaId);
	    }

		private bool HayInconsistencia(FechaVM vm)
	    {
		    return HayEquiposRepetidos(vm) || FaltanEquipos(vm) || HayLocalSinVisitanteOVicerversa(vm);
	    }

	    private bool HayLocalSinVisitanteOVicerversa(FechaVM vm)
	    {
			for (var i = 0; i < vm.Locales.Length; i++)
			{
				if ((vm.Locales[i] != 0 && vm.Visitantes[i] == 0) || (vm.Locales[i] == 0 && vm.Visitantes[i] != 0))
				{
					ModelState.AddModelError("", "Si se ingresa el equipo local, debe ingresarse el visitante y viceversa.");
					return true;
				}
			}
		    return false;
	    }

	    private bool FaltanEquipos(FechaVM vm)
	    {
		    var equiposSeleccionados = vm.Locales.Concat(vm.Visitantes).ToList();
		    var equiposDeLaZona = EquiposDeLaZona(vm.ZonaId).Select(x => x.Id).ToList();

		    foreach (var equipoDeLaZona in equiposDeLaZona)
		    {
			    if (equipoDeLaZona > 0 && !equiposSeleccionados.Contains(equipoDeLaZona))
			    {
					ModelState.AddModelError("", "Falta ingresar al menos un equipo de la zona.");
				    return true;
				}				    
		    }
		    return false;
	    }

		private bool HayEquiposRepetidos(FechaVM vm)
	    {
		    var locales = vm.Locales.ToList();
			var union = locales.Concat(vm.Visitantes).ToList();
			if (union.GroupBy(x => x).Where(group => group.Count() > 1 && group.Key != -1 && group.Key != -2 && group.Key != 0).Select(group => group.Key).Any())
		    {
			    ModelState.AddModelError("", "Se ingresó dos veces el mismo equipo.");
			    return true;
		    }
		    return false;
	    }

	    private List<IdDescripcionVM> EquiposDeLaZona(int zonaId)
	    {
		    var zonaHelper = new ZonaHelper(Context);
		    var zona = Context.Zonas.Find(zonaId);
		    
			var result = zonaHelper.EquiposDeLaZona(zona)
				.Select(x => new IdDescripcionVM
				{
					Id = Convert.ToInt32(x.Value),
					Descripcion = x.Text
				})
				.ToList();		   

			result.Add(new IdDescripcionVM
			{
				Id = -1,
				Descripcion = "LIBRE"
			});

		    result.Add(new IdDescripcionVM
		    {
			    Id = -2,
			    Descripcion = "INTERZONAL"
		    });

		    return result;
	    }

	    public override ActionResult Details(int id)
	    {
		    var model = Context.Fechas.Find(id);

		    var vm = VMM.MapForDetailsCustom(model);

		    return View(vm);
	    }

	    [HttpPost]
	    public ActionResult PublicarQuitar(int id)
	    {
		    var model = Context.Fechas.Find(id);

		    if (model != null)
			    model.Publicada = !model.Publicada;

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	    [HttpPost]
	    public ActionResult Reiniciar(int id)
	    {
		    var fecha = Context.Fechas.Find(id);
			var jornadas = Context.Jornadas.Where(x => x.FechaId == id);
		    var partidos = Context.Partidos.Where(x => jornadas.Select(j => j.Id).Contains(x.JornadaId));

		    Context.Partidos.RemoveRange(partidos);
		    Context.Jornadas.RemoveRange(jornadas);
			VMM.MapForReiniciar(fecha);	//No le cabió el ID=0 para el local y el visitante.

			Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }


	}
}