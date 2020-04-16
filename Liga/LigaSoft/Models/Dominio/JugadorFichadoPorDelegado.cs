using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.Dominio
{
	public class JugadorFichadoPorDelegado
	{
		[Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[Required, MaxLength(9), RegularExpression(@"^[0-9]*$"), Index(IsUnique = true)]
		public string DNI { get; set; }

		[Column(TypeName = "VARCHAR")]
		[Required, MaxLength(14)]
		public string Nombre { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(14)]
		[Required]
		public string Apellido { get; set; }

		[Required]
		public DateTime FechaNacimiento { get; set; }

		[Required]
		public int EquipoId { get; set; }
		public virtual Equipo Equipo { get; set; }

		[Required]
		public EstadoJugadorFichadoPorDelegado Estado { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(150)]
		public string MotivoDeRechazo { get; set; }
	}
}