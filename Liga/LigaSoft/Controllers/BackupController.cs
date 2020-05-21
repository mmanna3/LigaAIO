using System.Web.Mvc;
using LigaSoft.Utilidades.Backup;

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
	}
}