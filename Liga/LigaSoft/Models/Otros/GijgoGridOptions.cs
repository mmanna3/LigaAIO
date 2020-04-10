namespace LigaSoft.Models.Otros
{
	public class GijgoGridOptions
	{
		public string SearchField;
		public string SearchValue;
		public string Direction;
		public string SortBy;
		public int? Limit;
		public int? Page;

		public GijgoGridOptions(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue)
		{
			Page = page;
			Limit = limit;
			SortBy = sortBy;
			Direction = direction;
			SearchField = searchField;
			SearchValue = searchValue;
		}
	}
}