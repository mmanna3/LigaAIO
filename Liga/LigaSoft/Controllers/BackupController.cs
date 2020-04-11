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
			new ImagenesGDriveBackupManager().GenerarYSubirAlDrive();

		    new BaseDeDatosGDriveBackupManager().GenerarYSubirAlDrive();

			return Json("", JsonRequestBehavior.AllowGet);
		}
	}
}