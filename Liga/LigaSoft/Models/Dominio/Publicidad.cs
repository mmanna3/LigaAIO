using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.Dominio
{
	public class Publicidad
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, StringLength(40)]
		public string Titulo { get; set; }

		[StringLength(200)]
		public string Url { get; set; }

		public PublicidadPosicion Posicion { get; set; }
	}
}