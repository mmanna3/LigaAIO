using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LigaSoft.Models.Otros;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridBuilder<TModel> : UIBuilder
	{		
		private readonly List<GridColumnText<TModel>> _textColumns;
		private readonly List<GridColumnIcon<TModel>> _iconColumns;
		private readonly List<GridColumnButton<TModel>> _buttonColumns;
		private readonly IList<GijgoGridFilter> _dataFilters;
		private readonly GridComboActionFactory _actions;

		private int _pageLimit = 10;
		private string _dataSource;
		private string _primaryKey = "Id";
		private string _name = "grid";
		private readonly HtmlHelper<TModel> _helper;
		private string _checkbox;
		private string _dataSortBy = "";
		private string _parentId = "";

		public GridBuilder(HtmlHelper<TModel> helper)
		{
			_textColumns = new List<GridColumnText<TModel>>();
			_iconColumns = new List<GridColumnIcon<TModel>>();
			_buttonColumns = new List<GridColumnButton<TModel>>();
			_dataFilters = new List<GijgoGridFilter>();
			_actions = new GridComboActionFactory();
			_helper = helper;			
		}

		public GridBuilder<TModel> PageLimit(int limit)
		{
			_pageLimit = limit;
			return this;
		}

		public GridBuilder<TModel> Name(string name)
		{
			_name = name;
			return this;
		}

		public GridBuilder<TModel> DataSource(string dataSource)
		{
			_dataSource = dataSource;
			return this;
		}

		public GridBuilder<TModel> ParentId(int id)
		{
			_parentId = $"&parentId={id}";
			return this;
		}

		public GridBuilder<TModel> DataFilter(string field, object value)
		{
			var filter = new GijgoGridFilter(field, value);
			_dataFilters.Add(filter);
			return this;
		}

		public GridBuilder<TModel> DataSort(string sortField, string direction = "desc")
		{
			_dataSortBy = $"&sortBy={sortField}&direction={direction}";
			return this;
		}

		public GridBuilder<TModel> DataFilter(string field, string @operator, int value)
		{
			var filter = new GijgoGridFilter(field, @operator, value);
			_dataFilters.Add(filter);
			return this;
		}

		public GridBuilder<TModel> PrimaryKey(string primaryKey)
		{
			_primaryKey = primaryKey;
			return this;
		}

		public GridBuilder<TModel> WithColumns(Action<GridColumnFactory<TModel>> bindAllColumns)
		{
			var columnBinder = new GridColumnFactory<TModel>(_helper, _textColumns, _iconColumns, _buttonColumns);
			bindAllColumns(columnBinder);
			return this;
		}

		public GridBuilder<TModel> Actions(Action<GridComboActionFactory> bindAllActions)
		{			
			bindAllActions(_actions);
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='gj-clear-both'></div>
						<div class='gj-margin-top-12'>
						<table id ='{_name}'></table >
					</div>" 
					+ 
					ScriptString();
		}

		public string ScriptString()
		{
			return $@"<script>
						$(document).ready(function () {{

							grid = $('#{_name}').grid({{
								uiLibrary: 'bootstrap',
								primaryKey: '{_primaryKey}',
								dataSource: '{_dataSource}?{FiltersToQueryParams()}{_dataSortBy}{_parentId}',
								locale: 'es-es',
								{_checkbox}
								columns: [{ColumnsJs()}],								
								pager: {{ limit: {_pageLimit} }}
							}});
							grid.on('rowDataBound', function (e, $row, id, record) {{
								/*Para el dropdown-menu de las Acciones*/
								jQuery($row.find('.dropdown > ul > li > a')).attr(""data-id"", id);								
							}});
						}});

						{string.Join(" ", _iconColumns.Select(x => x.JavaScriptToAppend()))}
						{string.Join(" ", _buttonColumns.Select(x => x.JavaScriptToAppend()))}
						{_actions.JavaScriptToAppend()}
					</script>";
		}

		private string FiltersToQueryParams()
		{
			var result = "";
			if (_dataFilters.Any())
			{
				for (var i = 0; i < _dataFilters.Count; i++)
				{
					result += $"&filters[{i}].field={_dataFilters[i].field}&filters[{i}].operator={_dataFilters[i].@operator}&filters[{i}].value={_dataFilters[i].value}";
				}

				return result;
			}
			return result;
		}

		private string ColumnsJs()
		{
			return $"{string.Join(",", _textColumns.Select(x => x.ToJsColumn()))}," +
					$"{IconColumns()}" +
					$"{ButtonColumns()}" +
					$"{_actions.ToJs()}";
		}

		private string IconColumns()
		{
			return _iconColumns.Any() ? $"{string.Join(",", _iconColumns.Select(x => x.ToJsColumn()))}," : string.Empty;
		}

		private string ButtonColumns()
		{
			return _buttonColumns.Any() ? $"{string.Join(",", _buttonColumns.Select(x => x.ToJsColumn()))}" : string.Empty;
		}

		public GridBuilder<TModel> Checkbox()
		{
			_checkbox = $@"	selectionMethod: 'checkbox',
							selectionType: 'multiple',";
			return this;
		}
	}
}