using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.ExtensionMethods;
using LigaSoft.Utilidades;

namespace LigaSoft.Models.Dominio
{
	public class JugadorEquipo
	{
		[Key, Column(Order = 0)]
		public int JugadorId { get; set; }		
		[Key, Column(Order = 1)]
		public int EquipoId { get; set; }

		public virtual Jugador Jugador { get; set; }
		public virtual Equipo Equipo { get; set; }		

		public DateTime FechaFichaje { get; set; }

		public bool FueMigrado { get; set; }

		public bool EstaSuspendido { get; set; }

		public string Descripcion()
		{
			return $"DNI: {Jugador.DNI} - {Jugador.Apellido.ToCamelCase()}, {Jugador.Nombre.ToCamelCase()} - Categoría: {Jugador.Categoria()} - Fichado: {DateTimeUtils.ConvertToString(FechaFichaje)}";
		}
	}
}