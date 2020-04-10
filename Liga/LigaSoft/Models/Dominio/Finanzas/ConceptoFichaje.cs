using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("ConceptoFichaje")]
	public class ConceptoFichaje : Concepto
	{
		public override string Tipo()
		{
			return "Fichaje";
		}
	}

}