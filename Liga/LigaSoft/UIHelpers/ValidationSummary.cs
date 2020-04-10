using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Xml.Linq;

namespace LigaSoft.UIHelpers
{
	public class ValidationSummary : UIBuilder
	{
		private readonly HtmlHelper _helper;
		private string _classes = string.Empty;
		private bool _excludePropertyErrors = true;

		public ValidationSummary(HtmlHelper helper)
		{
			_helper = helper;
		}

		public override string ToHtmlString()
		{
			var htmlString = _helper.ValidationSummary(_excludePropertyErrors, "", new { @class = $@"alert alert-danger {_classes}" });

			if (htmlString != null)
			{
				var xEl = XElement.Parse(htmlString.ToHtmlString());
				var lis = xEl.Element("ul")?.Elements("li");
				if (lis.Count() == 1 && lis.First().Value == "")
					return null;

				return htmlString.ToString();
			}
			return string.Empty;
		}

		public ValidationSummary Classes(string classes)
		{
			_classes += classes;
			return this;
		}

		public ValidationSummary NotExcludePropertyErrors()
		{
			_excludePropertyErrors = false;
			return this;
		}
	}
}