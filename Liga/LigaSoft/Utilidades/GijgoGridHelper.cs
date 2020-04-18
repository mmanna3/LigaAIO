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
			query = ApplyFilters(query, options);
			query = ApplySearch(query, options);
			query = ApplySort(query, options);
			total = query.Count();
			query = ApplyPageAndLimit(query, options);

			return query;
		}

		private static IQueryable<T> ApplyPageAndLimit<T>(IQueryable<T> query, GijgoGridOpciones options)
			where T : class
		{
			if (options.page.HasValue && options.limit.HasValue)
			{
				var start = (options.page.Value - 1) * options.limit.Value;
				return query.Skip(start).Take(options.limit.Value);
			}

			return query;
		}

		private static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, GijgoGridOpciones options) 
			where T : class
		{
			if (options.filters != null)
				foreach (var filter in options.filters)
					query = query.Where($"{filter.field} {filter.@operator} {filter.value}").AsQueryable();

			return query;
		}

		private static IQueryable<T> ApplySort<T>(IQueryable<T> query, GijgoGridOpciones options) where T : class
		{
			if (!string.IsNullOrEmpty(options.sortBy) && !string.IsNullOrEmpty(options.direction))
				return query.OrderBy(options.direction.Trim().ToLower() == "asc" ? options.sortBy : $"{options.sortBy} desc");

			return query.OrderBy("Id desc");
		}

		private static IQueryable<T> ApplySearch<T>(IQueryable<T> query, GijgoGridOpciones options) 
			where T : class
		{
			if (!string.IsNullOrWhiteSpace(options.searchValue))
				return query.Where($"{options.searchField}.Contains(@0)", options.searchValue);

			return query;
		}
	}
}