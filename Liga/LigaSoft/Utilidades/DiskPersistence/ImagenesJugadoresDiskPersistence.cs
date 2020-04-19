using System.Drawing;
using System.IO;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.DiskPersistence
{
	public class ImagenesJugadoresDiskPersistence : IImagenesJugadoresPersistence
	{
		private static AppPaths Paths;

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

		//No testeado
		public void GuardarFotoDeJugadorDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);
			vm.Foto.SaveAs(imagePath);
		}

		public string GetFotoEnBase64(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";
			using (var stream = new FileStream(imagePath, FileMode.Open))
			using (var image = Image.FromStream(stream))
				return ImagenUtility.ImageToBase64(image);
		}

		public string Path(string dni)
		{
			return $"{Paths.ImagenesJugadoresRelative}/{dni}.jpg";
		}

		//No testeado
		public void GuardarImagenJugadorImportado(string dni, byte[] fotoByteArray)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);

			using (var image = Image.FromStream(new MemoryStream(fotoByteArray)))
			{
				image.Save(imagePath);
			}
		}

		public void Eliminar(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);
		}
	}

	public interface IImagenesJugadoresPersistence
	{
		void GuardarFotoWebCam(JugadorBaseVM vm);
		void GuardarFotoDeJugadorDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm);
		string GetFotoEnBase64(string dni);
		void GuardarImagenJugadorImportado(string dni, byte[] fotoByteArray);
		void Eliminar(string dni);
		string Path(string dni);
	}
}