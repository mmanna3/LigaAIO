using System.Web.Mvc;
using LigaSoft.Scheduler;

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