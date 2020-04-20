using System;
using System.IO;
using Ionic.Zip;

namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class BackupDiskPersistence : IBackupPersistence
	{
		private static AppPaths Paths;

		public BackupDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}

		// ReSharper disable AssignNullToNotNullAttribute
		public string ComprimirImagenesYPonerZipEnCarpetaDeBackups()
		{
			Directory.CreateDirectory(Paths.BackupAbsolute());
			var backupPath = Paths.BackupImagenes();

			try
			{
				using (var zip = new ZipFile())
				{
					zip.AddDirectory(Paths.ImagenesAbsolute);
					Log.Info($"Se comprimió correctamente la carpeta '{Paths.ImagenesAbsolute}'.");
					zip.Save(backupPath);
					Log.Info($"Se guardó la carpeta comprimida en '{backupPath}'.");
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error comprimiendo imágenes");
			}

			return backupPath;
		}		

		public string ComprimirUltimoBackupBdYPonerZipEnCarpetaDeBackups()
		{
			var bdBackupPathSinComprimir = PathDelUltimoBackupBD();
			if (bdBackupPathSinComprimir == null)
				YKNExHandler.LoguearYLanzarExcepcion(new Exception(), "No hay backups de base de datos");
			else
				Log.Info($"El backup sin comprimir de la base está en: {bdBackupPathSinComprimir}.");

			var backupBdComprimidoPath = Paths.BackupBaseDeDatos();

			try
			{
				using (var zip = new ZipFile())
				{
					zip.AddFile(bdBackupPathSinComprimir);
					Log.Info($"Se comprimió correctamente el archivo '{bdBackupPathSinComprimir}'.");
					zip.Save(backupBdComprimidoPath);
					Log.Info($"Se guardó el archivo comprimido en '{backupBdComprimidoPath}'.");
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error comprimiendo base de datos");
			}

			return backupBdComprimidoPath;
		}

		public void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups()
		{
			EliminarArchivos(Paths.BackupAbsolute(), "*.*");
		}

		private static string PathDelUltimoBackupBD()
		{
			var backupPaths = Directory.GetFiles(Paths.BackupAbsolute(), "mmmannna3_edefi_prod_*.bak");

			DateTime? fechaDelBackupMasNuevo = null;
			string pathDelBackupMasNuevo = null;
			foreach (var backupPath in backupPaths)
			{
				var fechaDeEsteBackup = File.GetCreationTime(backupPath);

				if (fechaDelBackupMasNuevo == null || fechaDeEsteBackup > fechaDelBackupMasNuevo)
				{
					fechaDelBackupMasNuevo = fechaDeEsteBackup;
					pathDelBackupMasNuevo = backupPath;
				}					
			}

			return pathDelBackupMasNuevo;
		}

		private static void EliminarArchivos(string folderPath, string fileSearchPattern)
		{
			Log.Info($"Se van a eliminar todos los archivos de '{folderPath}' con extensión: '{fileSearchPattern}'.");
			var filePaths = Directory.GetFiles(folderPath, fileSearchPattern);
			Log.Info($"Cantidad de archivos con extensión '{fileSearchPattern}' encontrados en la carpeta: '{filePaths.Length}'.");
			foreach (var filePath in filePaths)
			{
				Log.Info($"Se intenta eliminar el archivo: '{filePath}'.");
				if (File.Exists(filePath))
				{
					try
					{
						File.Delete(filePath);
						Log.Info("Se eliminó correctamente.");
					}
					catch (Exception ex)
					{
						YKNExHandler.LoguearYLanzarExcepcion(ex, "Error al intentar eliminar el archivo.");
					}
				}
				else
				{
					Log.Info("El archivo no existía.");
				}

			}
		}
	}
}