using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Interfaces;

namespace LigaSoft.Models.Dominio
{
	public class Torneo : IClassConIdDescripcion
	{
		public int Id { get; set; }

		[NotMapped]
		public string Descripcion
		{
			get => DescripcionParaCombo();
			set => throw new System.NotImplementedException();
		}

		[Required, Index("IX_AnioYTipo", 1, IsUnique = true)]
		public Anio Anio { get; set; }

		[Required, Index("IX_AnioYTipo", 2, IsUnique = true)]
		public int TipoId { get; set; }

		public virtual TorneoTipo Tipo { get; set; }

		public bool Publico { get; set; }
		public bool SancionesHabilitadas { get; set; }

		public virtual ICollection<Equipo> Equipos { get; set; }
		public virtual ICollection<Zona> Zonas { get; set; }
		public virtual ICollection<Categoria> Categorias { get; set; }

		private string DescripcionParaCombo()
		{
			return $"{Tipo.Descripcion} {Anio.Descripcion()}";
		}
	}
}