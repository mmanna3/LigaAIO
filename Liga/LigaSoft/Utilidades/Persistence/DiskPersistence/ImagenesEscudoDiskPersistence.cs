using System.IO;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class ImagenesEscudosDiskPersistence : IImagenesEscudosPersistence
	{
		private static AppPaths Paths;

		public ImagenesEscudosDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}

		public void Guardar(CargarEscudoVM vm)
		{
			var imagePath = $"{Paths.ImagenesEscudosAbsolute}/{vm.ClubId}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesEscudosAbsolute);
			vm.Escudo.SaveAs(imagePath);
		}

		public void Eliminar(int id)
		{
			var imagePath = $"{Paths.ImagenesEscudosAbsolute}/{id}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);
		}

		public string Path(int clubId, string escudoPorDefecto)
		{
			var escudoPathRelativo = $"{Paths.ImagenesEscudosRelative}/{clubId}.jpg";
			var escudoPathAbsoluto = $"{Paths.ImagenesEscudosAbsolute}/{clubId}.jpg";
			if (File.Exists(escudoPathAbsoluto))
				return escudoPathRelativo;

			return ImagenUtility.ProcesarImagenDeBDParaMostrarEnWeb(escudoPorDefecto);
		}
	}
}