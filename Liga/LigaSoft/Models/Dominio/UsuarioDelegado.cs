﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
	public class UsuarioDelegado
	{	
		public int Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(30)]
		public string Usuario { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(50)]
		public string Email { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(50)]
		public string Password { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(50)]
		public string Nombre { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(50)]
		public string Apellido { get; set; }

		public bool BlanqueoDeClavePendiente { get; set; }

		[Required]
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }

		public string AspNetUserId { get; set; }
		public virtual ApplicationUser AspNetUser { get; set; }

		public bool Aprobado { get; set; }
	}
}