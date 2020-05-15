using System;
using System.IO;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;
using Tests.Integration.Utilidades;
using Tests.Unit;

namespace Tests.Integration
{
	[TestFixture]
	public class BackupDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly BackupDiskPersistence _backupDiskPersistence;

		public BackupDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_backupDiskPersistence = new BackupDiskPersistence(new AppPathsForTest());
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
		public void ComprimirImagenesYPonerZipEnCarpetaDeBackups()
		{
			Directory.CreateDirectory(_paths.ImagenesAbsolute);
			var backupImagenes = _backupDiskPersistence.ComprimirImagenesYPonerZipEnCarpetaDeBackups();

			Assert.AreEqual(true, File.Exists(backupImagenes));
		}

		[Test]
		public void LanzaExcepcionSiNoHayBackupBd()
		{
			Assert.That(() => _backupDiskPersistence.ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups(),
				Throws.Exception
					.TypeOf<Exception>()
					.With.Property("Message")
					.Contains("No hay backups de base de datos"));
		}

		[Test]
		public void ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups()
		{
			var backupBdPath = $"{_paths.BackupAbsolute()}/mmmannna3_edefi_prod_12231.bak";

			var fs = File.Create(backupBdPath);
			fs.Close();		

			var pathBackupComprimido = _backupDiskPersistence.ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups();

			Assert.AreEqual(true, File.Exists(pathBackupComprimido));
		}

		[Test]
		public void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups()
		{
			var backupBdPath = $"{_paths.BackupAbsolute()}/mmmannna3_edefi_prod_12231.bak";

			var fs = File.Create(backupBdPath);
			fs.Close();

			_backupDiskPersistence.EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups();

			Assert.AreEqual(0, Directory.GetFiles(_paths.BackupAbsolute()).Length);
		}
	}
}
