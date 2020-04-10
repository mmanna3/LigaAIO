using LigaSoft.ExtensionMethods;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridColumnText<TModel>
	{
		private string _title;
		private string _sortable = "true";
		private string _propertyName;
		private string _width = string.Empty;


		public GridColumnText<TModel> Title(string title)
		{
			_title = title;
			return this;
		}

		public GridColumnText<TModel> Sorteable(bool sortable)
		{
			_sortable = sortable.ToJavaScript();
			return this;
		}

		public GridColumnText(string propertyName, string defaultLabel)
		{
			_propertyName = propertyName;
			_title = defaultLabel;
		}

		public GridColumnText<TModel> Width(int width)
		{
			_width = $"width: {width},";
			return this;
		}

		public string ToJsColumn()
		{
			return $@"{{ {_width} field: '{_propertyName}', sortable: {_sortable}, title: '{_title}'}}";
		}
	}
}