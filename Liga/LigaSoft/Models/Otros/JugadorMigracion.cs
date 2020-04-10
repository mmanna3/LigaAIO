using System;

namespace LigaSoft.Models.Otros
{
	public class JugadorMigracion
	{
		public string DNI { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public DateTime FechaFichaje { get; set; }
		public int EquipoId { get; set; }
	}	
}