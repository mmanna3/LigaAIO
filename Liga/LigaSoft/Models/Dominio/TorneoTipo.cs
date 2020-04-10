using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;
using LigaSoft.Models.Interfaces;

namespace LigaSoft.Models.Dominio
{
	public class TorneoTipo : IClassConIdDescripcion
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public string LoQueSeImprimeEnElCarnet { get; set; }
		public int ValidezDelCarnetEnAnios { get; set; }
		public TorneoFormato Formato { get; set; }
	}
}