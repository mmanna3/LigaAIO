using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class ZonaRelampagoEquipo
	{
		[Key, Column(Order = 0)]
		public int ZonaId { get; set; }		
		[Key, Column(Order = 1)]
		public int EquipoId { get; set; }

		public virtual Zona Zona { get; set; }
		public virtual Equipo Equipo { get; set; }		
	}
}