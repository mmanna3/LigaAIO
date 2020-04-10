using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Equipo
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, StringLength(18)]
		public string Nombre { get; set; }

		[Required]
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }

		public int? TorneoId { get; set; }
		public virtual Torneo Torneo { get; set; }

		public int? ZonaId { get; set; }
		public virtual Zona Zona { get; set; }

		public virtual ICollection<JugadorEquipo> JugadorEquipo { get; set; }
		public virtual ICollection<Jornada> JornadasDeLocal { get; set; }
		public virtual ICollection<Jornada> JornadasDeVisitante { get; set; }

		public virtual ICollection<ZonaRelampagoEquipo> ZonaRelampagoEquipo { get; set; }

		public int? Delegado1Id { get; set; }
		public virtual Delegado Delegado1 { get; set; }

		public int? Delegado2Id { get; set; }
		public virtual Delegado Delegado2 { get; set; }

		public int ValorDeLaCuota { get; set; }

		public string Descripcion()
		{
			if (Torneo != null)
				return Zona != null
					? $"{Nombre} - {Zona.DescripcionCompleta()} - CUOTA: ${ValorDeLaCuota}"
					: $"{Nombre} - TORNEO: {Torneo.Descripcion}";
			return Nombre;
		}

		public string CantidadFichados()
		{
			return JugadorEquipo.Count.ToString();
		}
	}
}