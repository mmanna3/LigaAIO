using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace LigaSoft.Utilidades.Backup
{
	public static class GoogleDriveBackupManager
	{		
		private static readonly YKNGoogleDriveService YKNDriveService = new YKNGoogleDriveService();

		public static void GenerarBackupImagenes()
		{
			try
			{
				Log.Info("Se comienza la generación del backup de imágenes");
				var imagenesBackupPath = IODiskUtility.ComprimirImagenesYPonerZipEnAppData();

				EliminarDelDriveBackupImagenesMasAntiguoSiHayMasDe3();

				SubirBackupImagenesAlDrive(imagenesBackupPath);
				Log.Info("Finaliza la generación del backup de imágenes");
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		public static void GenerarBackupBaseDeDatos()
		{
			try
			{
				Log.Info("Se comienza la generación del backup de base de datos");
				var bdBackupPath = IODiskUtility.ComprimirUltimoBackupBdYPonerZipEnAppData();

				EliminarDelDriveBackupBDMasAntiguoSiHayMasDe3();

				SubirBackupBdAlDrive(bdBackupPath);
				Log.Info("Finaliza la generación del backup de base de datos");
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

		private static void EliminarDelDriveBackupBDMasAntiguoSiHayMasDe3()
		{			
			EliminarDelDriveBackupMasAntiguoSiHayMasDe3("BaseDeDatos", ".zip");
		}

		private static void SubirBackupImagenesAlDrive(string filePath)
		{
			YKNDriveService.SubirArchivo(filePath, Path.GetFileName(filePath), "application/octet-stream");
		}

		private static void SubirBackupBdAlDrive(string filePath)
		{
			YKNDriveService.SubirArchivo(filePath, Path.GetFileName(filePath), "application/octet-stream");
		}

		private static void EliminarDelDriveBackupImagenesMasAntiguoSiHayMasDe3()
		{
			EliminarDelDriveBackupMasAntiguoSiHayMasDe3("Imagenes", ".zip");
		}

		private static void EliminarDelDriveBackupMasAntiguoSiHayMasDe3(string fileNameStartWith, string fileNameEndsWith)
		{
			var files = YKNDriveService.ListAll().Where(x => x.Name.StartsWith(fileNameStartWith) && x.Name.EndsWith(fileNameEndsWith)).OrderBy(x => x.CreatedTime).ToList();

			if (files.Count >= 3)
				YKNDriveService.DeleteFile(files.First().Id);			
		}
	}
}