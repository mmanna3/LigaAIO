using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class QuitaDePuntos
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, Index("IX_ZonaYCategoriaYEquipo", 1, IsUnique = true)]
		public int ZonaId { get; set; }
		public virtual Zona Zona { get; set; }

		[Required, Index("IX_ZonaYCategoriaYEquipo", 2, IsUnique = true)]
		public int CategoriaId { get; set; }
		public virtual Categoria Categoria { get; set; }

		[Required, Index("IX_ZonaYCategoriaYEquipo", 3, IsUnique = true)]
		public int EquipoId { get; set; }
		public virtual Equipo Equipo { get; set; }
		public int? CantidadDePuntosDescontados { get; set; }
	}
}