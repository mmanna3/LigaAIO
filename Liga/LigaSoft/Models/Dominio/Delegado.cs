using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LigaSoft.Models.Interfaces;

namespace LigaSoft.Models.Dominio
{
	public class Delegado : IClassConIdDescripcion
	{	
		public int Id { get; set; }

		[Required]
		public string Descripcion { get; set; }

		[Required, StringLength(20), RegularExpression(@"^[0-9]*$")]
		public string Telefono { get; set; }

		public bool BlanqueoDeClavePendiente { get; set; }

		[Required]
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }

		[InverseProperty("Delegado1")]
		public virtual ICollection<Equipo> EquiposDelegado1 { get; set; }

		[InverseProperty("Delegado2")]
		public virtual ICollection<Equipo> EquiposDelegado2 { get; set; }
	}

}