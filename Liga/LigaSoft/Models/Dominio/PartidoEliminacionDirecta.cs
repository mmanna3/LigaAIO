using LigaSoft.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class PartidoEliminacionDirecta
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, Index("IX_TorneoYCategoriaYFaseYOrden", 1, IsUnique = true)]
		public int TorneoId { get; set; }
		public virtual Torneo Torneo { get; set; }

		[Required, Index("IX_TorneoYCategoriaYFaseYOrden", 2, IsUnique = true)]
		public int CategoriaId { get; set; }
		public virtual Categoria Categoria { get; set; }

		[Required, Index("IX_TorneoYCategoriaYFaseYOrden", 3, IsUnique = true)]
		public FaseDeEliminacionDirectaEnum Fase { get; set; }

		[Required, Index("IX_TorneoYCategoriaYFaseYOrden", 4, IsUnique = true)]
		public int Orden { get; set; }

		public virtual Equipo Local { get; set; }
		public int? LocalId { get; set; }

		public virtual Equipo Visitante { get; set; }
		public int? VisitanteId { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesLocal { get; set; }

		[RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesVisitante { get; set; }

		public int? PenalesLocal { get; set; }

		public int? PenalesVisitante { get; set; }
	}
}