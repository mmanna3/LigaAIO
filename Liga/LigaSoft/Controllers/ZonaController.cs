using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Builders;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class ZonaController : ABMControllerWithParent<Zona, ZonaVM, ZonaVMM, Torneo, TorneoVM, TorneoVMM>
    {
	    public ZonaController() : base("Torneo","TorneoId")
	    {
	    }

	    protected override void BeforeReturningCreateView(ZonaVM vm)
	    {
		    vm.Torneo = Context.Torneos.Find(vm.TorneoId)?.Descripcion;
		    MapTiposDeZonasDisponibles(vm);
	    }

		public ActionResult ModificarEquipos(int parentId, int id)
		{
			var zona = Context.Zonas.Find(id);
			var zonaHelper = new ZonaHelper(Context);
			var equiposDeLaZona = zonaHelper.EquiposDeLaZona(zona);
			var equiposDelTorneoSinZona = zonaHelper.EquiposDelTorneoSinZona(zona);

			var vm = new ModificarEquiposVM(id, zona?.Nombre, zona.Torneo.Id, zona.Torneo.Descripcion, equiposDeLaZona, equiposDelTorneoSinZona);

			return View(vm);
		}

		[HttpPost]
		public ActionResult ModificarEquipos(ModificarEquiposVM vm)
		{
			var zona = Context.Zonas.Find(vm.ZonaId);
			var formato = zona.Torneo.Tipo.Formato;

			if (formato.Equals(TorneoFormato.AperturaClausura))
				UpdateContextAperturaClausura(vm);
			else if (formato.Equals(TorneoFormato.Relampago))
				UpdateContextRelampago(vm, zona);

			Context.SaveChanges();

			return RedirectTo("Index", vm.TorneoId);
		}

	    private void UpdateContextRelampago(ModificarEquiposVM vm, Zona zona)
	    {
		    if (vm.EquiposDeLaZonaResult != null)
			    foreach (var equipoId in vm.EquiposDeLaZonaResult)
			    {
				    var equipo = Context.Equipos.Find(equipoId);
				    if (Context.ZonaRelampagoEquipos.SingleOrDefault(x => x.EquipoId == equipoId && x.ZonaId == zona.Id) == null)
					    Context.ZonaRelampagoEquipos.Add(new ZonaRelampagoEquipo {Equipo = equipo, Zona = zona});
			    }

		    if (vm.EquiposDelTorneoSinZonaResult != null)
			    foreach (var equipoId in vm.EquiposDelTorneoSinZonaResult)
			    {
				    var zonaRelampagoEquipo = Context.ZonaRelampagoEquipos.SingleOrDefault(x => x.EquipoId == equipoId && x.ZonaId == zona.Id);
					if (zonaRelampagoEquipo != null)
					    Context.ZonaRelampagoEquipos.Remove(zonaRelampagoEquipo);
			    }
		}

	    private void UpdateContextAperturaClausura(ModificarEquiposVM vm)
	    {
		    if (vm.EquiposDeLaZonaResult != null)
			    foreach (var equipoId in vm.EquiposDeLaZonaResult)
			    {
				    var equipo = Context.Equipos.Find(equipoId);
				    equipo.ZonaId = vm.ZonaId;
			    }

		    if (vm.EquiposDelTorneoSinZonaResult != null)
			    foreach (var equipoId in vm.EquiposDelTorneoSinZonaResult)
			    {
				    var equipo = Context.Equipos.Find(equipoId);
				    equipo.ZonaId = null;
			    }
	    }

	    public ActionResult Tablas(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    var builder = new TablaDashboardBuilder(Context);
		    var vm = builder.Tablas(zona);

		    return View(vm);
		}

	    public ActionResult ResumenDeJornadas(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    var resumenJornadasHelper = new ResumenDeJornadasBuilder(Context);
		    var fechas = zona.Fechas.Where(x => x.Jornadas.Any(y => y.Partidos.Any())).ToList();

			var vm = resumenJornadasHelper.Tablas(zona, fechas);

		    return View(vm);
	    }

		[HttpPost]
	    public ActionResult ResumenDeJornadas(ResumenDeJornadasVM vm)
		{
			var jornadas = Context.Jornadas;

			foreach (var jornadaVerificadaId in vm.JornadasVerificadasId)
			{
				var jornada = jornadas.Find(jornadaVerificadaId);
				jornada.ResultadosVerificados = true;
			}

			Context.SaveChanges();

			return RedirectToAction("ResumenDeJornadas", new {parentId = vm.TorneoId, id = vm.ZonaId});			
		}

		public ActionResult Fixture(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    var vm = VMM.MapFixturePanelAdministrativo(zona);

		    return View(vm);
	    }

	    public ActionResult DatosDeEquipos(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    var vm = VMM.MapDatosDeEquipos(zona);

		    return View(vm);
	    }

	    public ActionResult PartidosPostergadosOSuspendidos(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    var vm = VMM.MapPartidosPostergadosOSuspendidos(zona);

		    return View(vm);
	    }		

		public void MapTiposDeZonasDisponibles(ZonaVM vm)
	    {
		    var torneoFormato = Context.Torneos.Find(vm.TorneoId).Tipo.Formato;
		    VMM.MapTiposDeZonasDisponibles(torneoFormato, vm);
	    }

	    [HttpPost]
	    public ActionResult PublicarQuitarFixture(int id)
	    {
			var zona = Context.Zonas.Find(id);

		    zona.FixturePublicado = !zona.FixturePublicado;

			Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }

	    [HttpPost]
	    public ActionResult MostrarOcultarGolesEnLaTabla(int id)
	    {
		    var zona = Context.Zonas.Find(id);

		    zona.VerGolesEnTabla = !zona.VerGolesEnTabla;

		    Context.SaveChanges();

		    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
	    }
	}
} 