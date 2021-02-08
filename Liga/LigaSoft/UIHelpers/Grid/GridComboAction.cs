using System.Web;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridComboAction
	{
		private readonly string _label;
		private readonly string _dataUrl = string.Empty;
		private readonly string _onClick;
		private readonly UrlHelper _url = new UrlHelper(HttpContext.Current.Request.RequestContext);

		public GridComboAction(string label, string controller, string method)
		{
			_label = label;
			_dataUrl = $@"data-url=""{_url.Action(method, controller)}/""";
			_onClick = @"onclick=""redirectToDataUrlUsingDataIdAsParam(this)"""; //todo Hacer esto en el event Handler de Jquery	
		}
		
		public GridComboAction(string label, string jsFuncName)
		{
			_label = label;
			_onClick = $@"onclick=""{jsFuncName}(this)""";  //todo Hacer esto en el event Handler de Jquery
		}

		public GridComboAction(string label, object routeValues)
		{
			_label = label;
			_dataUrl = $@"data-url=""{_url.RouteUrl("ParentWithChild", routeValues)}/""";
			_onClick = @"onclick=""redirectToDataUrlUsingDataIdAsParam(this)"""; //todo Hacer esto en el event Handler de Jquery			
		}

		public GridComboAction(string label, string parentName, string controller, string method)
		{
			_label = label;
			_dataUrl = $@"data-parent=""/{parentName}/"" data-url=""/{controller}/{method}/""";
			_onClick = @"onclick=""redirectToParentDataUrlUsingDataIdAsParam(this)"""; //todo Hacer esto en el event Handler de Jquery			
		}

		public string ToHtmlString()
		{			
			return $@"<li role =""presentation""><a role =""menuitem"" tabindex =""-1"" {_dataUrl} href=""#"" {_onClick}>{_label}</a></li>";
		}
	}
}