using System.Web.Mvc;
using System.Web.Routing;

namespace LigaSoft
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute(
			//	"Public",
			//	"Index",
			//	new { controller = "Public", action = "Index" }
			//);

			routes.MapRoute(
				name: "Delegados",
				url: "delegados",
				defaults: new { controller = "UsuarioDelegado", action = "Registrar" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Public", action = "AppInit", id = UrlParameter.Optional }
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
