using System.IO;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class ImagenesPublicidadDiskPersistence : IImagenesPublicidadPersistence
	{
		private static AppPaths Paths;

		public ImagenesPublicidadDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}

		public void Guardar(PublicidadVM vm)
		{
			var imagePath = $"{Paths.ImagenesPublicidadesAbsolute}/{vm.Id}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesPublicidadesAbsolute);
			vm.ImagenNueva.SaveAs(imagePath);
		}

		public string Path(int id)
		{
			return $"{Paths.ImagenesPublicidadesRelative}/{id}.jpg";
		}
	}
}