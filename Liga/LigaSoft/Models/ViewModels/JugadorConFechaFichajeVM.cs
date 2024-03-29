﻿using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorConFechaFichajeVM : JugadorBaseVM
	{
		[Display(Name = "Fecha de fichaje")]
		public string FechaFichaje { get; set; }

		[Display(Name = "Estado")]
		public string EstadoDescripcion { get; set; }
	}
}