using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Sancion
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime Dia { get; set; }

		public virtual Jornada Jornada { get; set; }
		public int JornadaId { get; set; }

		public virtual Categoria Categoria { get; set; }
		public int CategoriaId { get; set; }

		[Required, StringLength(300)]
		public string Descripcion { get; set; }

		public int CantidadFechasQueAdeuda { get; set; }

		public bool Visible { get; set; }
	}
}