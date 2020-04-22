using System.IO;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenUtilityTest
	{
		private readonly AppPathsForTest _paths;
		private const string puntoRojoEnBase64ConUriScheme = "data:image/jpeg;base64, iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==";
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
		public void ProcesarFotoJpgBase64ParaGuardarEnDisco()
		{
			var bitMap = ImagenUtility.ProcesarFotoBase64ParaGuardarEnDisco(puntoRojoEnBase64ConUriScheme);

			bitMap.Save(_imagenJpgPath);

			Assert.AreEqual(true, File.Exists(_imagenJpgPath));
		}
	}
}
