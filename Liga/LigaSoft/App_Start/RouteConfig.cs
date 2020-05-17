using System.Web.Mvc;
using System.Web.Routing;

namespace LigaSoft
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Delegados",
				url: "delegados/{action}",
				defaults: new { controller = "UsuarioDelegado", action = "SeleccionarEquipo" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Publico", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "ParentWithChild",
				url: "{parent}/{parentId}/{controller}/{action}/{id}",
				defaults: new { id = UrlParameter.Optional },
				constraints: new { parentId = @"\d+" }
			);
		}
	}
}
