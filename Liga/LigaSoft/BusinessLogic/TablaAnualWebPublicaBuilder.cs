using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.BusinessLogic
{
    public class TablaAnualWebPublicaBuilder : TablaBuilderPadre
	{
		public TablaAnualWebPublicaBuilder(ApplicationDbContext context) : base(context)
		{
		}
		
		protected override void AgregarLeyendaSiLaHubiere(Zona zona, TablasVM vm)
		{
			var zonaTipoABuscar = zona.Tipo == ZonaTipo.Apertura ? ZonaTipo.Clausura : ZonaTipo.Apertura;
			
			var laOtraZona = Context.Zonas.Include(zona1 => zona1.ZonaCategorias).SingleOrDefault(x => x.TorneoId == zona.TorneoId && x.Tipo == zonaTipoABuscar && x.Nombre == zona.Nombre);	
			
			foreach (var tabla in vm.TablasPorCategoria)
			{
				var zonaCategoria = zona.ZonaCategorias.SingleOrDefault(x => x.CategoriaId == tabla.CategoriaId && x.EsAnual);
				tabla.Leyenda = zonaCategoria?.Leyenda;

				if (laOtraZona != null)
				{
					var zonaCategoriaDeLaOtraZona = laOtraZona.ZonaCategorias.SingleOrDefault(x => x.CategoriaId == tabla.CategoriaId && x.EsAnual);
					if (tabla.Leyenda == null)
						tabla.Leyenda = zonaCategoriaDeLaOtraZona?.Leyenda;
					else
						tabla.Leyenda += "\n"+zonaCategoriaDeLaOtraZona?.Leyenda;
				}
			}
		}

		protected override void DescontarPuntosSiHayQuitaDePuntos(Zona zonaApertura, TablasVM vm)
		{
			var zonasClausuraDelTorneo = Context.Zonas.SingleOrDefault(x => x.TorneoId == zonaApertura.TorneoId && x.Tipo == ZonaTipo.Clausura && x.Nombre == zonaApertura.Nombre);
			
			base.DescontarPuntosSiHayQuitaDePuntos(zonaApertura, vm);
			if (zonasClausuraDelTorneo != null)
				base.DescontarPuntosSiHayQuitaDePuntos(zonasClausuraDelTorneo, vm);
		}
		
		protected override IQueryable<Partido> PartidosDelEquipoEnLaZona(Zona zonaApertura, Equipo equipo)
		{
			var zonasClausuraDelTorneo = Context.Zonas.Where(x => x.TorneoId == zonaApertura.TorneoId && x.Tipo == ZonaTipo.Clausura);

			var zonaClausura = zonasClausuraDelTorneo.SingleOrDefault(x => x.Fechas.SelectMany(f => f.Jornadas).Select(j => j.LocalId).ToList().Contains(equipo.Id)) ??
			                   zonasClausuraDelTorneo.SingleOrDefault(x => x.Fechas.SelectMany(f => f.Jornadas).Select(j => j.VisitanteId).ToList().Contains(equipo.Id));

			if (zonaClausura == null)
				return Context.Partidos.Where(x => x.Jornada.Fecha.Publicada
												   && (x.Jornada.Fecha.Zona.Id == zonaApertura.Id)
												   && (x.Jornada.LocalId == equipo.Id || x.Jornada.VisitanteId == equipo.Id));

			return Context.Partidos.Where(x => x.Jornada.Fecha.Publicada
			                                    && (x.Jornada.Fecha.Zona.Id == zonaClausura.Id || x.Jornada.Fecha.Zona.Id == zonaApertura.Id)
			                                    && (x.Jornada.LocalId == equipo.Id || x.Jornada.VisitanteId == equipo.Id));
		}
	}
}