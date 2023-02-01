using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

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

		public List<EquipoDelJugadorVM> Equipos { get; set; }

		[YKNRequired]
		public string Foto { get; set; }
	}
}

public class EquipoDelJugadorVM {
	
	public int EquipoId { get; set; }
	public string Nombre { get; set; }
	public string Torneo { get; set; }
	public string Zona { get; set; }
	public string FechaFichaje { get; set; }
	public EstadoJugador Estado { get; set; }
	public int TarjetasAmarillas { get; set; }
	public int TarjetasRojas { get; set; }
}