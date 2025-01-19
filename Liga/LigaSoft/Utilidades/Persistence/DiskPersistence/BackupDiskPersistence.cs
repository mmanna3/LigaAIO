using System;
using System.Diagnostics;
using System.IO;
using Ionic.Zip;
using LigaSoft.Models;

namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class BackupDiskPersistence : IBackupPersistence
	{
		private static AppPaths Paths;

		public BackupDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}
		
		public string ComprimirImagenesYPonerZipEnCarpetaDeBackups()
		{
			Directory.CreateDirectory(Paths.BackupAbsolute());
			var backupPath = Paths.BackupImagenes();

			try
			{
				using (var zip = new ZipFile())
				{
					zip.UseZip64WhenSaving = Zip64Option.Always;
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

		public string ComprimirBackupBaseDeDatosYPonerZipEnCarpetaDeBackups()
		{
			Directory.CreateDirectory(Paths.BackupAbsolute());
			var backupPath = Paths.BackupBaseDeDatos();

			try
			{
				using (var zip = new ZipFile())
				{
					zip.AddDirectory(Paths.CarpetaTemporalBackupBaseDeDatosAbsolute);
					Log.Info($"Se comprimió correctamente la carpeta '{Paths.CarpetaTemporalBackupBaseDeDatosAbsolute}'.");
					zip.Save(backupPath);
					Log.Info($"Se guardó el archivo comprimido en '{backupPath}'.");
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error comprimiendo base de datos");
			}

			return backupPath;
		}

		public void GenerarBackupDeBaseDeDatosEnCarpetaTemporal()
		{
			var backupDirectory = Paths.CarpetaTemporalBackupBaseDeDatosAbsolute;
			var connectionString = new ApplicationDbContext().Database.Connection.ConnectionString;

			var startInfo = new ProcessStartInfo
			{
				CreateNoWindow = false,
				UseShellExecute = false,
				FileName = Paths.BackupGeneratorExeAbsolute ?? throw new InvalidOperationException(),
				WindowStyle = ProcessWindowStyle.Hidden,
				Arguments = $@"script -c ""{connectionString}"" --dataTablesPattern "".*"" -d  ""{backupDirectory}"" -v -o"
			};

			try
			{
				using (var exeProcess = Process.Start(startInfo))
				{
					exeProcess?.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error generando backup de base de datos.");
			}
		}

		public void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups()
		{
			EliminarArchivos(Paths.BackupAbsolute(), "*.*");
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