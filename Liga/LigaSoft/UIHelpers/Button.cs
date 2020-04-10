using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers
{
	public class Button : UIBuilder
	{
		private string _label;
		private readonly string _id;
		private string _type = "type='button'";
		private string _classes = "btn ";
		private BootstrapColorEnum _color = BootstrapColorEnum.Primary;
		private string _onClick = string.Empty;
		private string _funcionJsParaImprimir = string.Empty;		
		private readonly UrlHelper _url;
		private HtmlHelper _helper;

		public Button(HtmlHelper helper, string id)
		{
			_url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			_id = id.Split()[0];
			_label = id;
			_helper = helper;
		}

		public Button Label(string label)
		{
			_label = label;			
			return this;
		}

		public Button Submit()
		{
			_type = "type='submit'";
			return this;
		}

		public Button Classes(string classes)
		{
			_classes += classes;
			return this;
		}

		public Button Color(BootstrapColorEnum color)
		{
			_color = color;
			return this;
		}

		public Button OnClick(string javascript)
		{
			_onClick = javascript;
			return this;
		}

		public Button OnClickRedirect(string action, string controller)
		{
			_onClick = $"window.location.href = '{_url.Action(action, controller)}';";
			return this;
		}

		public Button OnClickRedirect(string action, string controller, Dictionary<string, string> paramDictionary)
		{
			var parameters = FormatearParametrosParaUrl(paramDictionary);

			_onClick = $"window.location.href = '{_url.Action(action, controller)}{parameters}';";
			return this;
		}

		public object OnClickRedirect(string routeName, object routeValues)
		{			
			_onClick = $"window.location.href = '{_url.RouteUrl(routeName, routeValues)}';";
			return this;
		}

		public Button OnClickVolverAlIndice()
		{
			_onClick = $"window.location.href = '{HttpContext.Current.Request.UrlReferrer}';";			
			return this;
		}

		private void ColorToClass()
		{
			switch (_color)
			{
				case BootstrapColorEnum.Primary:
					_classes += " btn-primary";
					break;
				case BootstrapColorEnum.Success:
					_classes += " btn-success";
					break;
				case BootstrapColorEnum.Warning:
					_classes += " btn-warning";
					break;
				case BootstrapColorEnum.Danger:
					_classes += " btn-danger";
					break;
				case BootstrapColorEnum.Default:
					_classes += " btn-default";
					break;
			}
		}

		public override string ToHtmlString()
		{
			ColorToClass();

			return $@"<input class='{_classes}' id='{_id}' {_type} value='{_label}'>" + JavaScript();
		}

		private string JavaScript()
		{
			return $@"	<script>
							$(document).ready(function() {{	
								$('#{_id}').on('click', function() {{
									 {_onClick}
								}});
							}});
							{_funcionJsParaImprimir}
						</script>";
		}

		public Button PullRight()
		{
			_classes += "pull-right-button";
			return this;
		}

		public Button PullLeft()
		{
			_classes += "pull-left-button";
			return this;
		}

		public Button FullWidth()
		{
			_classes += "full-width";
			return this;
		}

		public Button OnClickImprimir(string divAImprimirId)
		{
			_funcionJsParaImprimir = @"	
			function printContent(el) {{
				var printcontent = document.getElementById(el).innerHTML;
				document.body.innerHTML = printcontent;
				window.print();
				location.reload();
			}}";
			
			return OnClick($"printContent('{divAImprimirId}')");
		}

		public Button OnClickImprimir()
		{
			_funcionJsParaImprimir = @"	
			function printContent() {{
				window.print();
				location.reload();
			}}";

			return OnClick("printContent()");
		}
	}
}