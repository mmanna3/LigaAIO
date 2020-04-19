using System.IO;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.DiskPersistence
{
	public class ImagenesJugadoresDiskPersistence : IImagenesJugadoresPersistence
	{
		private static AppPaths Paths = new AppPathsWebApp();

		public ImagenesJugadoresDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}

		public void GuardarFotoWebCam(JugadorBaseVM vm)
		{
			var foto = ImagenUtility.ProcesarImagenDeCamaraWebParaGuardarEnDisco(vm.Foto);
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);		
			foto.Save(imagePath);
		}
	}

	public interface IImagenesJugadoresPersistence
	{
		void GuardarFotoWebCam(JugadorBaseVM vm);
	}
}