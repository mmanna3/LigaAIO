using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class EditFotoJugadorDesdeArchivoVM : ViewModelConId
	{		
		public string DNI { get; set; }

		public string Nombre { get; set; }

		public string Apellido { get; set; }

		public string FechaNacimiento { get; set; }

		public List<string> Equipos { get; set; }

		public HttpPostedFileBase Foto { get; set; }
	}
}