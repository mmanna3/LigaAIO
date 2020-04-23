using System.IO;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesEscudosDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesEscudosDiskPersistence _imagenesEscudosDiskPersistence;
		private const int CLUBID = 1;
		private static string _escudoPath;
		private static readonly string ESCUDOBASE64 = Constantes.puntoRojoBase64;

		public ImagenesEscudosDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesEscudosDiskPersistence = new ImagenesEscudosDiskPersistence(_paths);
			_escudoPath = $"{_paths.ImagenesEscudosAbsolute}/{CLUBID}.jpg";
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
			GuardarEscudoRandomEnDisco(CLUBID);
			Assert.AreEqual(true, File.Exists(_escudoPath));
		}

		[Test]
		public void Eliminar()
		{
			GuardarEscudoRandomEnDisco(CLUBID);

			_imagenesEscudosDiskPersistence.Eliminar(CLUBID);

			Assert.AreEqual(false, File.Exists(_escudoPath));
		}

		[Test]
		public void GuardarEscudoDefault()
		{
			Assert.AreEqual(false, File.Exists(_paths.EscudoDefaultFileAbsolute));
			_imagenesEscudosDiskPersistence.GuardarEscudoDefault(ESCUDOBASE64);
			Assert.AreEqual(true, File.Exists(_paths.EscudoDefaultFileAbsolute));
		}

		[Test]
		public void SiEscudoNoExisteDevuelvePathDeEscudoDefault()
		{
			Assert.AreEqual(false, File.Exists(_escudoPath));
			Assert.AreEqual(_paths.EscudoDefaultRelative, _imagenesEscudosDiskPersistence.PathRelativo(CLUBID));
		}

		[Test]
		public void SiEscudoExisteDevuelveSuPath()
		{
			GuardarEscudoRandomEnDisco(CLUBID);
			Assert.AreEqual($"{_paths.ImagenesEscudosRelative}/{CLUBID}.jpg", _imagenesEscudosDiskPersistence.PathRelativo(CLUBID));
		}

		private void GuardarEscudoRandomEnDisco(int clubId)
		{
			var testImageFile = new HttpPostedFileRandomJpg();

			var vm = new CargarEscudoVM
			{
				ClubId = clubId,
				Escudo = testImageFile
			};

			_imagenesEscudosDiskPersistence.Guardar(vm);
		}
	}
}
