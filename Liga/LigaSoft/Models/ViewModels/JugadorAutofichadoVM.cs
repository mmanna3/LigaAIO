﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorAutofichadoVM : ViewModelConId
	{		
		[YKNRequired, YKNStringLength(Maximo = 9), RegularExpression(@"^[0-9]*$", ErrorMessage = "El DNI sólo puede contener números")]
		public string DNI { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Nombre { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Apellido { get; set; }

		[YKNRequired, YKNDateTime]
		public string FechaNacimiento { get; set; }

		public string CodigoAlfanumerico { get; set; }

		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public string Club { get; set; }

		public EstadoJugadorAutofichado Estado { get; set; }

		[Display(Name = "Estado")]
		public string EstadoDescripcion { get; set; }

		public string FotoCarnet { get; set; }

		public string FotoDNIFrente { get; set; }

		// Para la edición del delegado
		public HttpPostedFileBase ArchivoDeFotoDNIFrente { get; set; }

		public string FotoDNIDorso { get; set; }

		public string FotoCarnetRelativePath { get; set; }

		public string FotoDNIFrenteRelativePath { get; set; }

		public string FotoDNIDorsoRelativePath { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(150)]
		[Display(Name = "Motivo de rechazo")]
		public string MotivoDeRechazo { get; set; }				
	}
}