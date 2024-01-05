using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.Dominio
{
	public class Zona
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, StringLength(18)]
		public string Nombre { get; set; }

		[Required]
		public int TorneoId { get; set; }
		public virtual Torneo Torneo { get; set; }

		public virtual ICollection<Equipo> Equipos { get; set; }
		public virtual ICollection<Fecha> Fechas { get; set; }
		public virtual ICollection<ZonaCategoria> ZonaCategorias { get; set; }

		public ZonaTipo	Tipo { get; set; }

		public bool FixturePublicado { get; set; }

		public bool VerGolesEnTabla { get; set; }

		public bool TieneAlMenosUnaCategoriaUnaFechaYUnEquipo()
		{
			return Torneo.Categorias != null && Fechas != null && Equipos != null;
		}

		public string DescripcionCompleta()
		{
			return $"Torneo: {Torneo.Descripcion} - {Tipo.Descripcion()}. Zona: {Nombre} ";
		}
	}
}