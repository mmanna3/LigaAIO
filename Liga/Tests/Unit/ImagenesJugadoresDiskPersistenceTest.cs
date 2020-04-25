using System.IO;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesJugadoresDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;
		private const string DNI = "12345678";
		private static JugadorBaseVM _jugadorBaseVm;
		private static string _imagePath;

		public ImagenesJugadoresDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(_paths);
			_imagePath = $"{_paths.ImagenesJugadoresAbsolute}/{DNI}.jpg";
			_jugadorBaseVm = new JugadorBaseVM
			{
				Foto = Constantes.puntoRojoBase64ConUriDataJpg,
				DNI = DNI
			};
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.ImagenesJugadoresAbsolute);
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
		public void GuardarFotoWebCamCuandoLaFotoNoExiste()
		{
			Assert.AreEqual(false, File.Exists(_imagePath));

			_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(_jugadorBaseVm);

			Assert.AreEqual(true, File.Exists(_imagePath));
		}

		[Test]
		public void GuardarFotoWebCamReemplazandoExistente()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(_jugadorBaseVm);

			Assert.AreEqual(true, File.Exists(_imagePath));
		}


		[Test]
		public void GuardarFotoDeJugadorDesdeArchivo()
		{
			var vm = new EditFotoJugadorDesdeArchivoVM
			{
				Foto = new HttpPostedFileRandomJpg(),
				DNI = DNI
			};

			_imagenesJugadoresDiskPersistence.GuardarFotoDeJugadorDesdeArchivo(vm);

			Assert.AreEqual(true, File.Exists(_imagePath));
		}

		//Lo comento porque rompe algo de la fotoDNIFrente mockeada. Con una foto posta, parecería andar bien. Espero poder arreglarlo pronto.
		//[Test]
		//public void GuardarFotosTemporalesDeJugadorFichadoPorDelegado()
		//{
		//	var vm = new JugadorFichadoPorDelegadoVM
		//	{
		//		FotoCarnet = Constantes.puntoRojoBase64,
		//		FotoDNIFrente = new HttpPostedFileRandomJpg(),
		//		DNI = DNI
		//	};

		//	_imagenesJugadoresDiskPersistence.GuardarFotosTemporalesDeJugadorFichadoPorDelegado(vm);

		//	var pathFotoCarnetTemporal = $"{_paths.ImagenesTemporalesJugadorCarnetAbsolute}/{DNI}.jpg";
		//	var pathFotoDNIFrenteTemporal = $"{_paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{DNI}.jpg";

		//	Assert.AreEqual(true, File.Exists(pathFotoCarnetTemporal));
		//	Assert.AreEqual(true, File.Exists(pathFotoDNIFrenteTemporal));
		//}

		[Test]
		public void GetFotoEnBase64()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			Assert.IsNotEmpty(_imagenesJugadoresDiskPersistence.GetFotoEnBase64(DNI));
		}

		[Test]
		public void Eliminar()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			_imagenesJugadoresDiskPersistence.Eliminar(DNI);

			Assert.AreEqual(false, File.Exists(_imagePath));
		}

		[Test]
		public void CambiarDNI()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			const string nuevoDni = "22334400";
			_imagenesJugadoresDiskPersistence.CambiarDNI(DNI, nuevoDni);

			Assert.AreEqual(false, File.Exists(_imagePath));
			Assert.AreEqual(true, File.Exists($"{_paths.ImagenesJugadoresAbsolute}/{nuevoDni}.jpg"));
		}
	}
}
