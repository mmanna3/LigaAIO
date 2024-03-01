using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class EquipoEliminacionDirecta
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, Index("IX_TorneoYEquipo", 1, IsUnique = true)]
		public int TorneoId { get; set; }
		public virtual Torneo Torneo { get; set; }

		[Required, Index("IX_TorneoYEquipo", 2, IsUnique = true)]
		public int EquipoId { get; set; }
		public virtual Equipo Equipo { get; set; }
	}
}