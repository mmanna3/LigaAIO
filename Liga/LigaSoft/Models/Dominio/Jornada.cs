using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Dynamic;

namespace LigaSoft.Models.Dominio
{
	public class Jornada
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int FechaId { get; set; }
		public virtual Fecha Fecha { get; set; }

		public virtual Equipo Local { get; set; }
		public int? LocalId { get; set; }

		public virtual Equipo Visitante { get; set; }
		public int? VisitanteId { get; set; }

		public bool QuedoLibre { get; set; }
		public bool JuegaInterzonal { get; set; }

		public bool ResultadosVerificados { get; set; }

		public virtual ICollection<Partido> Partidos { get; set; }

		public string Descripcion()
		{
			return $@"{NombreDelLocal()} vs {NombreDelVisitante()}";
		}

		public int LocalIdInt()
		{
			if (LocalId != null)
				return (int)LocalId;

			return QuedoLibre ? -1 : -2;
		}

		public int VisitanteIdInt()
		{
			if (VisitanteId != null)
				return (int)VisitanteId;

			return QuedoLibre ? -1 : -2;
		}

		public string NombreDelVisitante()
		{
			if (Visitante != null)
				return Visitante.Nombre;

			return QuedoLibre ? "LIBRE" : "INTERZONAL";
		}

		public string NombreDelLocal()
		{
			if (Local != null)
				return Local.Nombre;

			return QuedoLibre ? "LIBRE" : "INTERZONAL";
		}

		public ICollection<Partido> PartidosSuspendidosOPostergados()
		{
			return Partidos.Where(x => x.EstaSuspendidoOPostergado()).ToList();
		}
	}
}