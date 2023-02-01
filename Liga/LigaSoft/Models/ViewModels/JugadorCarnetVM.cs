using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorCarnetVM
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string DNI { get; set; }
		public string Equipo { get; set; }
		public EstadoJugador Estado { get; set; }
		public string EstadoDescripcion { get; set; }
		public int TarjetasAmarillas { get; set; }
		public int TarjetasRojas { get; set; }
		public string FechaNacimiento { get; set; }
		public string FechaVencimiento { get; set; }
		public string TipoLiga { get; set; }
		public string Categoria { get; set; }
		public string FotoBase64 { get; set; }
		public string FotoPath { get; set; }
	}
}