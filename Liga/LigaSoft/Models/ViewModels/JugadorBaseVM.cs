using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorBaseVM : ViewModelConId
	{		
		[YKNRequired, YKNStringLength(Maximo = 9), RegularExpression(@"^[0-9]*$", ErrorMessage = "El DNI sólo puede contener números")]
		public string DNI { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Nombre { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Apellido { get; set; }

		[YKNRequired, YKNDateTime, Display(Name= "Fecha nacimiento")]
		public string FechaNacimiento { get; set; }

		[Display(Name = "Carnet impreso")]
		public string CarnetImpreso { get; set; }

		public bool CarnetImpresoBool { get; set; }

		public List<string> Equipos { get; set; }

		[YKNRequired]
		public string Foto { get; set; }
	}
}