using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio.Finanzas
{
	[Table("ConceptoInsumo")]
	public class ConceptoInsumo : Concepto
	{
		[Required]
		public int Precio { get; set; }

		[Required]
		public int Stock { get; set; }

		public override string Tipo()
		{
			return "Insumo";
		}

		public void IncrementarStock(int cantidad)
		{
			Stock += cantidad;

			if (Stock < 0)
				Stock = 0;
		}

		public void DecrementarStock(int cantidad)
		{
			Stock -= cantidad;

			if (Stock < 0)
				Stock = 0;
		}
	}

}