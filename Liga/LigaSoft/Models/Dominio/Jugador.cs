using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Utilidades;

namespace LigaSoft.Models.Dominio
{
	public class Jugador
	{
		[Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column(TypeName = "VARCHAR")]
		[Required, MaxLength(9), RegularExpression(@"^[0-9]*$"), Index(IsUnique = true)]
		public string DNI { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(14)]

		public string Nombre { get; set; }

		[Column(TypeName = "VARCHAR")]
		[MaxLength(14)]
		public string Apellido { get; set; }

		public DateTime FechaNacimiento { get; set; }

		public virtual ICollection<JugadorEquipo> JugadorEquipo { get; set; }

		public bool CarnetImpreso { get; set; }

		public bool FueMigrado { get; set; }

		public string Descripcion()
		{
			return $"DNI: {DNI} - {Apellido.ToCamelCase()}, {Nombre.ToCamelCase()} - Categoría: {Categoria()}";
		}

		public string Categoria()
		{
			return FechaNacimiento.Year.ToString().Substring(2);
		}

		public string FotoPath()
		{
			return IODiskUtility.FotoJugadorPath(DNI);
		}
	}
}