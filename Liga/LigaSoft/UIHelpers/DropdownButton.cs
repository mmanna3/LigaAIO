using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers
{
	public class DropdownButton : UIBuilder
	{
		private string _label;
		private string _classes = "btn ";
		private BootstrapColorEnum _color = BootstrapColorEnum.Primary;
		private string _onClick = string.Empty;	
		private readonly UrlHelper _url;
		private HtmlHelper _helper;
		private string _acciones = "";

		public DropdownButton(HtmlHelper helper)
		{
			_url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			_helper = helper;
		}

		public DropdownButton Label(string label)
		{
			_label = label;			
			return this;
		}

		public DropdownButton Color(BootstrapColorEnum color)
		{
			_color = color;
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


			return
				$@"<div class=""dropdown"">
						<button class=""btn {_classes} dropdown-toggle"" type=""button"" id=""menu1"" data-toggle=""dropdown"">
							{_label}
							<span class=""caret""></span>
						</button>
						<ul class=""dropdown-menu"" role=""menu"" aria-labelledby=""menu1"">
							 {_acciones}							
						</ul>
					</div>";
		}

		public DropdownButton FullWidth()
		{
			_classes += "full-width";
			return this;
		}

		public DropdownButton AddAction(string actionLabel, string action, string controller, Dictionary<string, string> paramDictionary)
		{
			var parameters = FormatearParametrosParaUrl(paramDictionary);			
			_acciones += $@"<li><a href='{_url.Action(action, controller)}{parameters}'>{actionLabel}</a></li>";

			return this;
		}
	}
}