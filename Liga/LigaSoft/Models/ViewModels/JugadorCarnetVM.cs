namespace LigaSoft.Models.ViewModels
{
	public class JugadorCarnetVM
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string DNI { get; set; }
		public string Equipo { get; set; }
		public string Estado { get; set; }
		public bool EstaSuspendido { get; set; }
		public string FechaNacimiento { get; set; }
		public string FechaVencimiento { get; set; }
		public string TipoLiga { get; set; }
		public string Categoria { get; set; }
		public string FotoBase64 { get; set; }
		public string FotoPath { get; set; }
	}
}