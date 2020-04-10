using System.Collections.Generic;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class DatosDeEquiposVM
	{
		public DatosDeEquiposVM(string titulo)
		{
			Renglones = new List<RenglonDatosEquipo>();
			Titulo = titulo;
		}

		public string Titulo { get; set; }
		public List<RenglonDatosEquipo> Renglones { get; set; }
		public int TorneoId { get; set; }
		public int ZonaId { get; set; }
	}

	public class RenglonDatosEquipo
	{
		public string Equipo { get; set; }
		public string Escudo { get; set; }
		public string Localidad { get; set; }
		public string Direccion { get; set; }
		public Techo Techo { get; set; }
		public string Delegado1 { get; set; }
		public string Telefono1 { get; set; }
		public string Delegado2 { get; set; }
		public string Telefono2 { get; set; }		
	}
}

