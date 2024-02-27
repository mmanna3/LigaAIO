using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.Dominio
{
	public class JugadorSancionadoDePorVida
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[Required, MaxLength(9), RegularExpression(@"^[0-9]*$"), Index(IsUnique = true)]
		public string DNI { get; set; }

		[MaxLength(14), Required]
		public string Nombre { get; set; }

		[MaxLength(14), Required]		
		public string Apellido { get; set; }

		[StringLength(300)]
		public string Motivo { get; set; }
	}
}