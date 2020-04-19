using System.IO;
using System.Linq;
using System.Web.Hosting;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;
using Tests.Integration;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesEscudosDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesEscudosDiskPersistence _imagenesJugadoresDiskPersistence;
		private const int CLUBID = 1;
		private static JugadorBaseVM _jugadorBaseVm;
		private static string _imagePath;

		public ImagenesEscudosDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesJugadoresDiskPersistence = new ImagenesEscudosDiskPersistence(_paths);
			_imagePath = $"{_paths.ImagenesEscudosAbsolute}/{CLUBID}.jpg";
			//_jugadorBaseVm = new CargarEscudoVM
			//{
			//	Escudo = ,
			//	ClubId = CLUBID
			//};
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.ImagenesEscudosAbsolute);
		}

		private static void EliminarTodosLosArchivosEnLaCarpeta(string path)
		{
			if (Directory.Exists(path))
			{
				var filePaths = Directory.GetFiles(path, "*");
				foreach (var filePath in filePaths)
					File.Delete(filePath);
			}
		}

		[Test]
		public void Guardar()
		{
			//Assert.AreEqual(false, File.Exists(_imagePath));

			//_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(_jugadorBaseVm);

			//Assert.AreEqual(true, File.Exists(_imagePath));
		}

		[Test]
		public void Eliminar()
		{
			//Guardar();

			//_imagenesJugadoresDiskPersistence.Eliminar(DNI);

			//Assert.AreEqual(false, File.Exists(_imagePath));
		}
	}
}
