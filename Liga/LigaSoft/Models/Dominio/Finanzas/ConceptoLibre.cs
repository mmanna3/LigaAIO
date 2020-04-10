using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("ConceptoLibre")]
	public class ConceptoLibre : Concepto
	{
		public override string Tipo()
		{
			return "Libre";
		}
	}

}