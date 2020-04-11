using System.Web.Mvc;
using LigaSoft.Utilidades.Backup;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class BackupController : Controller
    {
	    public JsonResult Generar()
	    {
		    Log.Info("------------------------------------------------");

			new ImagenesGDriveBackupManager().GenerarYSubirAlDrive();

		    new BaseDeDatosGDriveBackupManager().GenerarYSubirAlDrive();

		    IODiskUtility.EliminarTodosLosArchivosDeAppData();

		    Log.Info("------------------------------------------------");

			return Json("", JsonRequestBehavior.AllowGet);
		}
	}
}