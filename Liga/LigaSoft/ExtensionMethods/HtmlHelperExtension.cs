using System.Web.Mvc;
using LigaSoft.UIHelpers.WidgetFactories;

namespace LigaSoft.ExtensionMethods
{
    public static class HtmlHelperExtension
    {
        public static ITypedWidgetFactory<TModel> YKN<TModel>(this HtmlHelper<TModel> helper)
        {
            return new YKNTypedWidgetFactory<TModel>(helper);
        }

	    public static INonTypedWidgetFactory YKNNonTyped(this HtmlHelper helper)
	    {
		    return new YKNNonTypedWidgetFactory(helper);
	    }

		public static YKNTypedWidgetFactory<TModel> YKN<TModel>(this HtmlHelper helper) 
			where TModel : class, new()
		{
			var viewData = helper.ViewDataContainer.ViewData;
			var viewContext = helper.ViewContext;
			var routeCollection = helper.RouteCollection;			

			var newViewData = new ViewDataDictionary(viewData) { Model = new TModel() };
			var newViewContext = new ViewContext(
				viewContext.Controller.ControllerContext,
				viewContext.View,
				newViewData,
				viewContext.TempData,
				viewContext.Writer);
			var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);

			return new YKNTypedWidgetFactory<TModel>(new HtmlHelper<TModel>(newViewContext, viewDataContainer, routeCollection));
		}

	    private class ViewDataContainer : IViewDataContainer
	    {
		    public ViewDataDictionary ViewData { get; set; }

		    public ViewDataContainer(ViewDataDictionary viewData)
		    {
			    ViewData = viewData;
		    }
	    }
	}
}