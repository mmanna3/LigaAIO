using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Categoria
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public int Orden { get; set; }

		[Required, StringLength(18), Index("IX_NombreYTorneo", 1, IsUnique = true)]
		public string Nombre { get; set; }

		[Required, Index("IX_NombreYTorneo", 2, IsUnique = true)]
		public int TorneoId { get; set; }
		public virtual Torneo Torneo { get; set; }

		public virtual ICollection<Partido> Partidos { get; set; }
	}
}