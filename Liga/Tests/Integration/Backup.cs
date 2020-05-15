using System.IO;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;
using Tests.Integration.Utilidades;

namespace Tests.Integration
{
	[TestFixture]
	public class Backup
	{
		private readonly AppPathsForTest _paths;
		private readonly BackupDiskPersistence _backupDiskPersistence;

		public Backup()
		{
			_paths = new AppPathsForTest();
			_backupDiskPersistence = new BackupDiskPersistence(new AppPathsForTest());
		}

		//[SetUp]
		//public void Initialize()
		//{
		//	EliminarTodosLosArchivosEnLaCarpeta(_paths.BackupAbsolute());
		//}

		//private static void EliminarTodosLosArchivosEnLaCarpeta(string path)
		//{
		//	if (Directory.Exists(path))
		//	{
		//		var filePaths = Directory.GetFiles(path, "*");
		//		foreach (var filePath in filePaths)
		//			File.Delete(filePath);
		//	}
		//}

		//[Test]
		//public void ComprimirImagenesYPonerZipEnCarpetaDeBackups()
		//{
		//	Directory.CreateDirectory(_paths.ImagenesAbsolute);
		//	var backupImagenes = _backupDiskPersistence.ComprimirImagenesYPonerZipEnCarpetaDeBackups();

		//	Assert.AreEqual(true, File.Exists(backupImagenes));
		//}
	}
}
