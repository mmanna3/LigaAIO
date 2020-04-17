namespace LigaSoft.Models.Otros
{
	public class GijgoGridOpciones
	{
		public int? page { get; set; }
		public int? limit { get; set; }
		public string sortBy { get; set; }
		public string direction { get; set; }
		public string searchField { get; set; }
		public string searchValue { get; set; }
		public string filterField { get; set; }
		public string filterValue { get; set; }
		public string filterOperator { get; set; }
	}
}