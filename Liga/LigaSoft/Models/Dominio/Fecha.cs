using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LigaSoft.Models.Dominio
{
	public class Fecha
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int Numero { get; set; }

		public DateTime DiaDeLaFecha { get; set; }

		public int ZonaId { get; set; }
		public virtual Zona Zona { get; set; }

		public virtual ICollection<Jornada> Jornadas { get; set; }

		public int EquipoLibreId { get; set; }

		public bool Publicada { get; set; }

		public string Descripcion()
		{
			return $"Fecha {Numero}. {Zona.DescripcionCompleta()}";
		}
	}
}