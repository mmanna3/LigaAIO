using System;
using System.Linq;
using System.Web.Mvc;

public class AllowCrossSiteAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Methods", "*");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");

        if (filterContext.HttpContext.Request.HttpMethod == "OPTIONS")
        {
            filterContext.HttpContext.Response.Flush();
        }

        base.OnActionExecuting(filterContext);
    }

}