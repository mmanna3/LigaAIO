﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class ZonaCategoria
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, Index("IX_ZonaYCategoria", 1, IsUnique = true)]
		public int ZonaId { get; set; }
		public virtual Zona Zona { get; set; }

		[Required, Index("IX_ZonaYCategoria", 2, IsUnique = true)]
		public int CategoriaId { get; set; }
		public virtual Categoria Categoria { get; set; }

		[StringLength(2000)]
		public string Leyenda { get; set; }

		[Required, Index("IX_ZonaYCategoria", 3, IsUnique = true)]
		public bool EsAnual { get; set; }
	}
}