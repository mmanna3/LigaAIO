using System.Collections.Generic;
using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;

namespace LigaSoft.Builders
{
    public class TablaAnualWebPublicaBuilder : TablaBuilderPadre
	{
		public TablaAnualWebPublicaBuilder(ApplicationDbContext context) : base(context)
		{
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