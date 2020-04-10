using System.Web;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridColumnIcon<TModel>
	{
		private readonly HtmlHelper<TModel> _helper;
		private string _title = string.Empty;
		private string _jsFunc = string.Empty;
		private readonly string _glyphicon;
		private string _redirectController;
		private string _redirectMethod;

		public GridColumnIcon(HtmlHelper<TModel> helper, string glyphIcon)
		{
			_helper = helper;
			_glyphicon = glyphIcon;
		}

		public GridColumnIcon<TModel> JsFuncName(string jsFuncName)
		{
			_jsFunc = $", events: {{ 'click': {jsFuncName} }}";
			return this;
		}

		public GridColumnIcon<TModel> RedirectTo(string controller, string method)
		{
			_redirectController = controller;
			_redirectMethod = method;
			_jsFunc = $", events: {{ 'click': {method} }}";
			return this;
		}

		public string ToJsColumn()
		{
			return $@"{{ width: 64, tmpl: '<span class=""icono-grilla glyphicon {_glyphicon} gj-cursor-pointer""></span>', align: 'center' {_jsFunc} }}";
		}

		public string JavaScriptToAppend()
		{
			var u = new UrlHelper(HttpContext.Current.Request.RequestContext);

			return $@"	function {_redirectMethod}(e) {{
							window.location.href = '{u.Action(_redirectMethod, _redirectController)}/' + e.data.id;
						}}";
		}
	}
}