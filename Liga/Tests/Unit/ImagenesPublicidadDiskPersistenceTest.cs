using System;
using System.IO;
using System.Web;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesPublicidadDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesPublicidadDiskPersistence _imagenesPublicidadDiskPersistence;
		private const int PUBLICIDADID = 1;
		private static string _publicidadPath;

		public ImagenesPublicidadDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesPublicidadDiskPersistence = new ImagenesPublicidadDiskPersistence(_paths);
			_publicidadPath = $"{_paths.ImagenesPublicidadesAbsolute}/{PUBLICIDADID}.jpg";
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.ImagenesPublicidadesAbsolute);
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
			GuardarPublicidadRandomEnDisco(PUBLICIDADID);
			Assert.AreEqual(true, File.Exists(_publicidadPath));
		}

		private void GuardarPublicidadRandomEnDisco(int publicidadId)
		{
			var testImageFile = new HttpPostedFileRandomJpg();

			var vm = new PublicidadVM
			{
				Id = publicidadId,
				ImagenNueva = testImageFile
			};

			_imagenesPublicidadDiskPersistence.Guardar(vm);
		}

		//Si la publicidad no existe, se rompe todillo. Hay que arreglar eso algún día.
		[Test]
		public void SiPublicidadExisteDevuelveSuPath()
		{
			GuardarPublicidadRandomEnDisco(PUBLICIDADID);
			Assert.AreEqual($"{_paths.ImagenesPublicidadesRelative}/{PUBLICIDADID}.jpg", _imagenesPublicidadDiskPersistence.Path(PUBLICIDADID));
		}
	}
}
