using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridColumnFactory<TModel> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		public List<GridColumnText<TModel>> TextColumnsOfTheGrid { get; set; }
		public List<GridColumnIcon<TModel>> IconColumnsOfTheGrid { get; set; }
		public List<GridColumnButton<TModel>> ButtonColumns { get; set; }

		public GridColumnFactory(HtmlHelper<TModel> helper, List<GridColumnText<TModel>> textColumnsOfTheGrid, List<GridColumnIcon<TModel>> iconColumnsOfTheGrid, List<GridColumnButton<TModel>> buttonColumns)
		{
			TextColumnsOfTheGrid = textColumnsOfTheGrid;
			IconColumnsOfTheGrid = iconColumnsOfTheGrid;
			ButtonColumns = buttonColumns;
			_helper = helper;
		}

		protected GridColumnFactory(){}

		public GridColumnText<TModel> AddTextColumn<TProp>(Expression<Func<TModel, TProp>> ex)
		{
			var col = new GridColumnText<TModel>(PropertyName(ex), DefaultLabel(_helper, ex));
			TextColumnsOfTheGrid.Add(col);
			return col;
		}

		public GridColumnIcon<TModel> AddIconColumn(string glyphIcon)
		{
			var col = new GridColumnIcon<TModel>(_helper, glyphIcon);
			IconColumnsOfTheGrid.Add(col);
			return col;
		}

		public GridColumnButton<TModel> AddButtonColumn(string label, string jsFunc)
		{
			var col = new GridColumnButton<TModel>(_helper, label, jsFunc);
			ButtonColumns.Add(col);
			return col;
		}

		public GridColumnButton<TModel> AddButtonColumnUsingIdAsParam(string label, string controller, string method)
		{
			var col = new GridColumnButton<TModel>(_helper, label, controller, method);
			ButtonColumns.Add(col);
			return col;
		}

		public override string ToHtmlString()
		{
			return string.Empty;	
		}
	}
}