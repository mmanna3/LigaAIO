using System.Linq;
using System.Linq.Dynamic;
using LigaSoft.Models.Otros;

namespace LigaSoft.Utilidades
{
	public static class GijgoGridHelper
	{
		public static IQueryable<T> ApplyOptionsToQuery<T>(IQueryable<T> query, GijgoGridOpciones options, out int total)
			where T : class
		{
			if (options.filters != null)
				foreach (var filter in options.filters)
					query = query.Where($"{filter.field} {filter.@operator} {filter.value}").AsQueryable();

			if (!string.IsNullOrEmpty(options.filterField))
			{
				if (options.filterOperator == null)
					options.filterOperator = "==";

				query = query.Where($"{options.filterField} {options.filterOperator} {options.filterValue}").AsQueryable();
			}

			if (!string.IsNullOrWhiteSpace(options.searchValue))
				query = query.Where($"{options.searchField}.Contains(@0)", options.searchValue);

			if (!string.IsNullOrEmpty(options.sortBy) && !string.IsNullOrEmpty(options.direction))
				query = query.OrderBy(options.direction.Trim().ToLower() == "asc" ? options.sortBy : $"{options.sortBy} desc");
			else
				query = query.OrderBy("Id desc");

			total = query.Count();
			if (options.page.HasValue && options.limit.HasValue)
			{
				var start = (options.page.Value - 1) * options.limit.Value;
				return query.Skip(start).Take(options.limit.Value);
			}

			return query;
		}
	}
}