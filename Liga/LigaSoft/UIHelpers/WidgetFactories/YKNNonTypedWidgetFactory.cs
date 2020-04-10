using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Xml.Linq;

namespace LigaSoft.UIHelpers.WidgetFactories
{
    public class YKNNonTypedWidgetFactory : INonTypedWidgetFactory
    {
        private readonly HtmlHelper _helper;

        public YKNNonTypedWidgetFactory(HtmlHelper helper)
        {
            _helper = helper;
        }

	    public ValidationSummary ValidationSummary()
	    {
			return new ValidationSummary(_helper);
		}

	    public Button Button(string id)
	    {
		    return new Button(_helper, id);
	    }
	}
}