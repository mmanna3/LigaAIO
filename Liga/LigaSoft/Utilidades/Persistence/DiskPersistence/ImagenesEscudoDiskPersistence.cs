using System.Drawing.Imaging;
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

		public void GuardarEscudoDefault(string escudoBase64)
		{
			var escudoDefault = ImagenUtility.ProcesarImagenDeCamaraWebParaGuardarEnDisco(escudoBase64);

			if (File.Exists(Paths.EscudoDefaultFileAbsolute))
				File.Delete(Paths.EscudoDefaultFileAbsolute);

			Directory.CreateDirectory(Paths.ImagenesEscudosAbsolute);
			escudoDefault.Save(Paths.EscudoDefaultFileAbsolute, ImageFormat.Jpeg);
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

		public string Path(int clubId)
		{
			var escudoPathRelativo = $"{Paths.ImagenesEscudosRelative}/{clubId}.jpg";
			var escudoPathAbsoluto = $"{Paths.ImagenesEscudosAbsolute}/{clubId}.jpg";
			if (File.Exists(escudoPathAbsoluto))
				return escudoPathRelativo;

			return Paths.EscudoDefaultRelative;
		}
	}
}