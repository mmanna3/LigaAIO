using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;

namespace LigaSoft.BusinessLogic
{
    public class TablaDashboardBuilder : TablaBuilderPadre
	{
		public TablaDashboardBuilder(ApplicationDbContext context) : base(context)
		{
		}

		protected override IQueryable<Partido> PartidosDelEquipoEnLaZona(Zona zona, Equipo equipo)
		{
			return Context.Partidos.Where(x => x.Jornada.Fecha.Zona.Id == zona.Id && (x.Jornada.LocalId == equipo.Id || x.Jornada.VisitanteId == equipo.Id));
		}
	}
}