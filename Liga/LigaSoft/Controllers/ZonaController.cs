using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
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
	    private readonly CategoriaVMM _categoriaVMM;
	    public ZonaController() : base("Torneo","TorneoId")
	    {
		    _categoriaVMM = new CategoriaVMM(Context);
	    }

	    protected override void BeforeReturningCreateView(ZonaVM vm)
	    {
		    vm.Torneo = Context.Torneos.Find(vm.TorneoId)?.Descripcion;
		    MapTiposDeZonasDisponibles(vm);
	    }

	    public ActionResult AgregarLeyenda(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);
		    var categorias = zona.Torneo.Categorias;
		    var categoriasVM = _categoriaVMM.MapForGrid(categorias.ToList());
		    
		    var categoriasConLeyenda = new List<CategoriaConLeyendaVM>();
		    foreach (var cat in categorias)
		    {
			    var zonaCategoria = Context.ZonaCategorias.SingleOrDefault(x => x.ZonaId == id && x.CategoriaId == cat.Id);
			    // if (zonaCategoria != null)
				    categoriasConLeyenda.Add(new CategoriaConLeyendaVM(cat.Id, cat.Nombre, zonaCategoria?.Leyenda));
		    }
		    
		    var vm = new AgregarLeyendaVM(id, zona?.Nombre, zona.Torneo.Id, zona.Torneo.Descripcion, categoriasConLeyenda);

		    return View(vm);
	    }
	    
	    public ActionResult AgregarLeyendaAnual(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);
		    var categorias = zona.Torneo.Categorias;
		    
		    var categoriasConLeyenda = new List<CategoriaConLeyendaVM>();
		    foreach (var cat in categorias)
		    {
			    var zonaCategoria = Context.ZonaCategorias.SingleOrDefault(x => x.ZonaId == id && x.CategoriaId == cat.Id && x.EsAnual == true);
			    categoriasConLeyenda.Add(new CategoriaConLeyendaVM(cat.Id, cat.Nombre, zonaCategoria?.Leyenda));
		    }
		    
		    var vm = new AgregarLeyendaVM(id, zona?.Nombre, zona.Torneo.Id, zona.Torneo.Descripcion, categoriasConLeyenda);

		    return View(vm);
	    }
	    
	    [HttpPost]
	    public ActionResult AgregarLeyenda(AgregarLeyendaVM vm)
	    {
		    var modeloExistente = Context.ZonaCategorias.SingleOrDefault(x => x.ZonaId == vm.ZonaId && x.CategoriaId == vm.CategoriaId);

		    if (modeloExistente != null)
		    {
			    if (vm.Leyenda == null)
				    vm.Leyenda = "";
				    
			    modeloExistente.Leyenda = vm.Leyenda;
		    }
		    else
		    {
			    var zonaCategoria = new ZonaCategoria
			    {
				    ZonaId = vm.ZonaId,
				    CategoriaId = vm.CategoriaId,
				    Leyenda = vm.Leyenda
			    };
			    Context.ZonaCategorias.Add(zonaCategoria);    
		    }
		    
		    
		    Context.SaveChanges();

		    return RedirectTo("Index", vm.TorneoId);
	    }
	    
	    [HttpPost]
	    public ActionResult AgregarLeyendaAnual(AgregarLeyendaVM vm)
	    {
		    var modeloExistente = Context.ZonaCategorias.SingleOrDefault(x => x.ZonaId == vm.ZonaId && x.CategoriaId == vm.CategoriaId && x.EsAnual == true);

		    if (modeloExistente != null)
		    {
			    if (vm.Leyenda == null)
				    vm.Leyenda = "";
				    
			    modeloExistente.Leyenda = vm.Leyenda;
		    }
		    else
		    {
			    var zonaCategoria = new ZonaCategoria
			    {
				    ZonaId = vm.ZonaId,
				    CategoriaId = vm.CategoriaId,
				    Leyenda = vm.Leyenda,
				    EsAnual = true
			    };
			    Context.ZonaCategorias.Add(zonaCategoria);    
		    }
		    
		    
		    Context.SaveChanges();

		    return RedirectTo("Index", vm.TorneoId);
	    }
	    
	    public ActionResult QuitarPuntos(int parentId, int id)
	    {
		    var zona = Context.Zonas.Find(id);
		    var categoriasModel = zona.Torneo.Categorias;

		    var quitaPorCategoria = categoriasModel.Select(cat => new QuitaPorCategoriaVM(cat.Nombre, cat.Id,0)).ToList();

		    var tuplas = new List<EquipoCategoriaQuitaVM>();
		    var todosLosEquiposDeLaZona = zona.Equipos.Select(e => new SelectListItem { Text = e.Nombre, Value = e.Id.ToString() }).ToList();


		    foreach (var cat in categoriasModel)
		    {
			    var equiposConQuitaDePuntos = Context.QuitaDePuntos.Where(x => x.ZonaId == id && x.CategoriaId == cat.Id).ToList();
			    foreach (var equipo in equiposConQuitaDePuntos) 
				    tuplas.Add(new EquipoCategoriaQuitaVM(cat.Id, equipo.EquipoId, equipo.CantidadDePuntosDescontados));
		    }

		    var vm = new QuitaDePuntosVM(id, zona?.Nombre, zona.Torneo.Id, zona.Torneo.Descripcion, quitaPorCategoria, todosLosEquiposDeLaZona, tuplas);

		    return View(vm);
	    }
	    
	    [HttpPost]
	    public ActionResult QuitarPuntos(QuitaDePuntosVM vm)
	    {
		    foreach (var quitaPorCategoria in vm.QuitaPorCategorias)
		    {
			    var modeloExistente = Context.QuitaDePuntos.SingleOrDefault(x => x.ZonaId == vm.ZonaId && x.CategoriaId == quitaPorCategoria.CategoriaId && x.EquipoId == vm.EquipoId);

			    if (modeloExistente != null)
			    {
				    modeloExistente.CantidadDePuntosDescontados = quitaPorCategoria.QuitaDePuntos;
			    }
			    else
			    {
				    if (quitaPorCategoria.QuitaDePuntos > 0)
				    {
					    var quitaDePuntos = new QuitaDePuntos
					    {
						    ZonaId = vm.ZonaId,
						    CategoriaId = quitaPorCategoria.CategoriaId,
						    EquipoId = vm.EquipoId,
						    CantidadDePuntosDescontados = quitaPorCategoria.QuitaDePuntos,
					    };
					    Context.QuitaDePuntos.Add(quitaDePuntos); 
				    }
			    }
		    }
		    
		    Context.SaveChanges();

		    return RedirectTo("Index", vm.TorneoId);
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

		    var resumenJornadasHelper = new ResumenDeJornadasBuilder();
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