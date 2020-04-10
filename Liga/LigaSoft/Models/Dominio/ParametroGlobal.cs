using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LigaSoft.Models.Dominio
{
    public class ParametroGlobal
    {
	    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id { get; set; }

		public int ValorPorDefectoEnPesosDelConceptoFichaje { get; set; }

        public string EscudoPorDefectoEnBase64 { get; set; }
    }
}