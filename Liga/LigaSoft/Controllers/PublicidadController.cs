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
	public class PublicidadController : ABMController<Publicidad, PublicidadVM, PublicidadVMM>
	{
		private readonly IImagenesPublicidadPersistence _imagenesPublicidadPersistence;

		public PublicidadController()
		{
			_imagenesPublicidadPersistence = new ImagenesPublicidadDiskPersistence(new AppPathsWebApp());
		}

		[HttpPost, ExportModelStateToTempData]
		public override ActionResult Edit(PublicidadVM vm)
		{
			ValidarImagen(vm.ImagenNueva);
			if (!ModelState.IsValid)
				return RedirectToAction("Edit", new { id = vm.Id });

			var model = Context.Publicidades.Find(vm.Id);

			VMM.MapForEdit(vm, model);

			if (vm.ImagenNueva != null)
				_imagenesPublicidadPersistence.Guardar(vm);

			Context.SaveChanges();

			return RedirectToAction("Index");
		}

		private void ValidarImagen(HttpPostedFileBase imagen)
		{
			if (imagen != null)
				if (imagen.ContentLength == 0)
					ModelState.AddModelError("", "No se ha seleccionado una imagen.");
				else if (!"jpg".Equals(imagen.FileName.Substring(imagen.FileName.Length - 3, 3).ToLower()))
					ModelState.AddModelError("", "La imagen debe estar en formato JPG.");
				else
					using (var foto = System.Drawing.Image.FromStream(imagen.InputStream))
						if (foto.Height != 400 || foto.Width != 400)
							ModelState.AddModelError("", "El tamaño de la imagen debe ser de 100 x 100 px.");
		}
	}
}