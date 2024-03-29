﻿using LigaSoft.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	public class Pago
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public DateTime Fecha { get; set; }

		[Required]
		public int MovimientoEntradaConClubId { get; set; }
		public virtual MovimientoEntradaConClub Movimiento { get; set; }

		[Required]
		public int Importe { get; set; }

		[Required]
		public DateTime FechaAlta { get; set; }
		[Required]
		public string UsuarioAltaId { get; set; }
		public virtual ApplicationUser UsuarioAlta { get; set; }

		public DateTime? FechaAnulacion { get; set; }
		public string UsuarioAnulacionId { get; set; }
		public virtual ApplicationUser UsuarioAnulacion { get; set; }

		public string Comentario { get; set; }

		public bool Vigente { get; set; }

		[Required]
		public FormaDePago FormaDePago { get; set; }
	}

}