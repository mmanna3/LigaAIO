using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Goleador
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int PartidoId { get; set; }
		public virtual Partido Partido { get; set; }

		public int JugadorId { get; set; }
		public virtual Jugador Jugador { get; set; }

		public int EquipoId { get; set; }
		public virtual Equipo Equipo { get; set; }

		public int Cantidad { get; set; }
	}
}