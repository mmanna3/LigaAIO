using System;
using System.Diagnostics;
using System.Web.Hosting;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Scheduler;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class BackupController : Controller
	{
		public JsonResult Generar()
	    {
		    BackupBaseDeDatosYFileSystem.GenerarYSubirADrive();

			return Json("", JsonRequestBehavior.AllowGet);
		}

		public JsonResult Db()
		{
			LaunchCommandLineApp();

			return Json("Generado?", JsonRequestBehavior.AllowGet);
		}

		private static void LaunchCommandLineApp()
		{
			var backupDirectory = HostingEnvironment.MapPath("~/App_Data/BackupZenSchema");
			var connectionString = new ApplicationDbContext().Database.Connection.ConnectionString;

			var startInfo = new ProcessStartInfo
			{
				CreateNoWindow = false,
				UseShellExecute = false,
				FileName = HostingEnvironment.MapPath("~/SchemaZen.exe") ?? throw new InvalidOperationException(),
				WindowStyle = ProcessWindowStyle.Hidden,
				Arguments = $@"script -c ""{connectionString}"" --dataTablesPattern "".*"" -d  ""{backupDirectory}"" -v -o"
			};

			try
			{
				using (var exeProcess = Process.Start(startInfo))
				{
					exeProcess?.WaitForExit();
				}
			}
			catch
			{
				// Log error.
			}
		}
	}
}