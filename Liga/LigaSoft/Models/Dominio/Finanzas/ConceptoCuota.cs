using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("ConceptoCuota")]
	public class ConceptoCuota : Concepto
	{
		public override string Tipo()
		{
			return "Cuota";
		}
	}

}