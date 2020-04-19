using System.Web;
using System.Web.Mvc;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class ParametroGlobalController : ABMController<ParametroGlobal, ParametroGlobalVM, ParametroGlobalVMM>
	{
		private readonly IImagenesEscudosPersistence _imagenesEscudosPersistence;

		public ParametroGlobalController()
		{
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		}

		[HttpPost, ExportModelStateToTempData]
		public override ActionResult Edit(ParametroGlobalVM vm)
		{
			ValidarCargarEscudo(vm.EscudoNuevo);
			if (!ModelState.IsValid)
				return RedirectToAction("Edit", new { id = vm.Id });

			var model = Context.ParametrizacionesGlobales.Find(vm.Id);

			VMM.MapForEdit(vm, model);
			if (vm.EscudoNuevo != null)
				_imagenesEscudosPersistence.GuardarEscudoDefault(model.EscudoPorDefectoEnBase64);

			Context.SaveChanges();

			return RedirectToAction("Edit", new {vm.Id});
		}

		public ActionResult DescargarBackupDb()
		{
			var vm = new DescargarBackupResultadoVM();
			var fileNames = System.IO.Directory.GetFiles(Server.MapPath("~/App_Data/"),"*.bak"); //Quizás haya que filtrar por el backup más nuevo

			if (fileNames.Length > 0)
			{
				var filePath = fileNames[0];

				Response.Clear();
				Response.ContentType = "application/octet-stream";
				Response.AppendHeader("Content-Disposition", "filename=" + filePath);

				Response.TransmitFile(filePath);

				Response.End();
				vm.Texto = "Backup generado correctamente.";
			}
			else
			{
				vm.Texto = "No hay backups disponibles. Los miércoles y domingos se eliminan los backups antiguos. Esperar 24 horas.";
			}

			return View("DescargarBackupResultado", vm);
		}

		public ActionResult DescargarBackupImagenes()
		{
			var vm = new DescargarBackupResultadoVM {Texto = "Backup generado correctamente."};

			var filePath = IODiskUtility.ComprimirImagenesYPonerZipEnCarpetaDeBackups();

			Response.Clear();
			Response.ContentType = "application/octet-stream";
			Response.AppendHeader("Content-Disposition", "filename=" + filePath);

			Response.TransmitFile(filePath);

			Response.End();

			return View("DescargarBackupResultado", vm);
		}

		private void ValidarCargarEscudo(HttpPostedFileBase imagen)
		{
			if (imagen != null)
				if (imagen.ContentLength == 0)
					ModelState.AddModelError("", "No se ha seleccionado un escudo.");
				else if (!"jpg".Equals(imagen.FileName.Substring(imagen.FileName.Length - 3, 3).ToLower()))
					ModelState.AddModelError("", "La imagen debe estar en formato JPG.");
				else
					using (var foto = System.Drawing.Image.FromStream(imagen.InputStream))
						if (foto.Height != 100 || foto.Width != 100)
							ModelState.AddModelError("", "El tamaño del escudo debe ser de 100 x 100 px.");
		}
	}
}