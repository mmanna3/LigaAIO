﻿using System.Linq;
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