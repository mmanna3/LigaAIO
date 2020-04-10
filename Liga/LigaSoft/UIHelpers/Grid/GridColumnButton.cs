using System.Web;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridColumnButton<TModel>
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly string _jsFunc;
		private readonly string _label;
		private readonly string _redirectController;
		private readonly string _redirectMethod;
		private string _width = string.Empty;
		private readonly string _jsToAppend = string.Empty;

		public GridColumnButton(HtmlHelper<TModel> helper, string label, string jsFuncName)
		{
			_helper = helper;
			_label = label;
			_jsFunc = $", events: {{ 'click': {jsFuncName} }}";
		}

		public GridColumnButton(HtmlHelper<TModel> helper, string label, string controller, string method)
		{
			_helper = helper;
			_label = label;
			_jsFunc = $", events: {{ 'click': {method} }}";
			_redirectMethod = method;
			_redirectController = controller;
			_jsToAppend = JavaScriptForRedirect();
		}

		public string ToJsColumn()
		{
			return $@"{{ {_width} tmpl: '<a class=""btn btn-default boton-grilla"" role=""button"">{_label}</a>', align: 'center' {_jsFunc} }}";
		}

		public GridColumnButton<TModel> Width(int width)
		{
			_width = $"width: {width},";
			return this;
		}

		public string JavaScriptToAppend()
		{
			return _jsToAppend;
		}

		private string JavaScriptForRedirect()
		{
			var u = new UrlHelper(HttpContext.Current.Request.RequestContext);

			return $@"	function {_redirectMethod}(e) {{
							window.location.href = '{u.Action(_redirectMethod, _redirectController)}/' + e.data.id;
						}}";
		}
	}
}