using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class UsuarioDelegadoPendienteDeAprobacion
	{	
		public int Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(100)]
		public string Email { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(100)]
		public string Password { get; set; }

		[Required]
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }
	}
}