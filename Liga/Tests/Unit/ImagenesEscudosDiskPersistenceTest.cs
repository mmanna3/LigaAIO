using System;
using System.IO;
using System.Web;
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
		private static string _imagePath;

		public ImagenesEscudosDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesEscudosDiskPersistence = new ImagenesEscudosDiskPersistence(_paths);
			_imagePath = $"{_paths.ImagenesEscudosAbsolute}/{CLUBID}.jpg";
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
			Assert.AreEqual(true, File.Exists(_imagePath));
		}

		[Test]
		public void Eliminar()
		{
			GuardarEscudoRandomEnDisco(CLUBID);

			_imagenesEscudosDiskPersistence.Eliminar(CLUBID);

			Assert.AreEqual(false, File.Exists(_imagePath));
		}

		[Test]
		public void PathSiEscudoNoExiste()
		{
			//Guardar();
		}

		[Test]
		public void PathSiEscudoExiste()
		{
			//GuardarEscudoRandomEnDisco(CLUBID);
			//_imagenesEscudosDiskPersistence.Path(CLUBID);
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

	internal class HttpPostedFileRandomJpg : HttpPostedFileBase
	{
		private readonly Stream stream;

		private static MemoryStream GetRandomStream()
		{
			var byteBuffer = new byte[10];
			var rnd = new Random();
			rnd.NextBytes(byteBuffer);
			return new MemoryStream(byteBuffer);
		}

		public HttpPostedFileRandomJpg()
		{
			stream = GetRandomStream();
			ContentType = "image/jpeg";
			FileName = "test-file.jpg";
		}

		public override int ContentLength => (int)stream.Length;

		public override string ContentType { get; }

		public override string FileName { get; }

		public override Stream InputStream => stream;

		public override void SaveAs(string filename)
		{
			using (var file = File.Open(filename, FileMode.CreateNew))
				stream.CopyTo(file);
		}
	}
}
