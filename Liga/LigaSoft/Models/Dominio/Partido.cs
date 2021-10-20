using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Partido
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, Index("IX_JornadaYCategoria", 1, IsUnique = true)]
		public int JornadaId { get; set; }
		public virtual Jornada Jornada { get; set; }

		[Required, Index("IX_JornadaYCategoria", 2, IsUnique = true)]
		public int CategoriaId { get; set; }
		public virtual Categoria Categoria { get; set; }

		[Required, RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesLocal { get; set; }

		[Required, RegularExpression(@"(^[0-9]*$)|(NP)|(AR)|(S)|(P)")]
		public string GolesVisitante { get; set; }

		public virtual ICollection<Goleador> Goleadores { get; set; }

		public bool EstaSuspendidoOPostergado()
		{
			return "P".Equals(GolesLocal) || "S".Equals(GolesLocal);
		}

		public string Descripcion()
		{
			return $"Cat: {Categoria} Local: {GolesLocal} Visitante: {GolesVisitante}";
		}
	}
}