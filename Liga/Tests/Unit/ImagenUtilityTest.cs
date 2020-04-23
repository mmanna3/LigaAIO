using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using LigaSoft.Utilidades;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenUtilityTest
	{
		private readonly AppPathsForTest _paths;
		private readonly string _imagenJpgPath;

		public ImagenUtilityTest()
		{
			_paths = new AppPathsForTest();
			_imagenJpgPath = _paths.BackupAbsoluteOf("imagen.jpg");
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.BackupAbsolute());
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
		public void ConvertirABitMapYATamanio240X240()
		{
			var bitMap = ImagenUtility.ConvertirABitMapYATamanio240X240(Constantes.puntoRojoBase64ConUriDataJpg);
			Assert.AreEqual(240, bitMap.Width);
			Assert.AreEqual(240, bitMap.Height);
		}

		[Test]
		public void ImagenCuadradaRotarAHorizontalYComprimir()
		{
			//var bytes = Convert.FromBase64String(Constantes.rectanguloVerticalBase64);
			//var stream = new MemoryStream(bytes);

			//var bitMap = ImagenUtility.RotarAHorizontalYComprimir(stream);

			//Assert.IsTrue(bitMap.Width > bitMap.Height);
		}

	}
}
