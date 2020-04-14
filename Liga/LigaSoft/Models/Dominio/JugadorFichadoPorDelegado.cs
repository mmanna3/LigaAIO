using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[MaxLength(14)]
		public string Nombre { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(14)]
		public string Apellido { get; set; }

		public DateTime FechaNacimiento { get; set; }
	}
}