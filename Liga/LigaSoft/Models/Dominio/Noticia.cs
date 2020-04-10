using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class Noticia
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime Fecha { get; set; }

		[Required, StringLength(60)]
		public string Titulo { get; set; }

		[Required, StringLength(4000)]
		public string Cuerpo { get; set; }

		public bool Visible { get; set; }
	}
}