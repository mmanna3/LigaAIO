using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using LigaSoft.Utilidades.Backup;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class BackupController : Controller
	{
		public async Task<JsonResult> Generar()
	    {
		    await BackupBaseDeDatosYFileSystem.GenerarYSubirADrive();

			return Json("", JsonRequestBehavior.AllowGet);
		}
	}
}