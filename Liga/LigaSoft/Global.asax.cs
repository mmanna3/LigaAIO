using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LigaSoft.Migrations;
using LigaSoft.Models;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			log4net.Config.XmlConfigurator.Configure();

	        InicializarLaBaseDeDatos();
	        InicializarFileSystem();
		}

	    private static void InicializarFileSystem()
	    {
		    var imagenesEscudosDiskPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		    imagenesEscudosDiskPersistence.GuardarEscudoDefault(new ApplicationDbContext().ParametrizacionesGlobales.First().EscudoPorDefectoEnBase64);
		}

	    private static void InicializarLaBaseDeDatos()
	    {
			AplicarMigracionesPendientesSiLasHay();
	    }

	    private static void AplicarMigracionesPendientesSiLasHay()
	    {
		    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>("DefaultConnection"));

			var dbMigrator = new DbMigrator(new Configuration());
			if (dbMigrator.GetPendingMigrations().Any())
				dbMigrator.Update();
	    }
	    
	    protected void Application_Error(object sender, EventArgs e)
	    {
		    var ex = Server.GetLastError();
		    Log.Error("Error capturado en Global.asax. StatusCode: " + ((HttpException)ex).GetHttpCode(), ex);
	    }
    }
}
