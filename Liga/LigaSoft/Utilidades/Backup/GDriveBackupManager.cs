﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Utilidades.Backup
{
	public abstract class GDriveBackupManager
	{		
		private static readonly YKNGoogleDriveService YKNDriveService = new YKNGoogleDriveService();

		protected abstract string ComprimirYPonerZipEnAppData();

		protected abstract string NombreDelBackupZipeadoSinExtensionNiFecha();

		protected readonly IBackupPersistence BackupDiskPersistence;

		protected GDriveBackupManager()
		{
			BackupDiskPersistence = new BackupDiskPersistence(new AppPathsWebApp());
		}

		public virtual async Task GenerarYSubirAlDrive()
		{
			var nombreBackup = NombreDelBackupZipeadoSinExtensionNiFecha();
			try
			{
				Log.Info($"Comienza la generación del backup de '{nombreBackup}'.");

				var backupPath = ComprimirYPonerZipEnAppData();

				EliminarDelDriveBackupMasAntiguoSiHayMasDe3(NombreDelBackupZipeadoSinExtensionNiFecha(), ".zip");

				await SubirAlDrive(backupPath);

				Log.Info($"Finaliza la generación del backup de '{nombreBackup}'.");
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, $"Error al generar el backup de '{nombreBackup}'");
			}
		}

		protected static async Task SubirAlDrive(string filePath)
		{
			var fileName = Path.GetFileName(filePath);
			try
			{
				await YKNDriveService.SubirArchivoAsync(filePath, fileName, "application/octet-stream");
				Log.Info($"Se subió al Drive el archivo '{fileName}'");
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, $"Error subiendo al Drive el archivo '{fileName}'");
			}			
		}

		protected void EliminarDelDriveBackupMasAntiguoSiHayMasDe3(string fileNameStartWith, string fileNameEndsWith)
		{
			Log.Info($"Si en el Drive hay más de 3 backups de '{NombreDelBackupZipeadoSinExtensionNiFecha()}', se eliminará el más antiguo.");
			var files = YKNDriveService.ListAll().Where(x => x.Name.StartsWith(fileNameStartWith) && x.Name.EndsWith(fileNameEndsWith)).OrderBy(x => x.CreatedTime).ToList();

			if (files.Count >= 3)
			{
				try
				{
					YKNDriveService.DeleteFile(files.First().Id);
					Log.Info($"Se eliminó el backup de nombre '{files.First().Name}'.");
				}
				catch (Exception e)
				{
					YKNExHandler.LoguearYLanzarExcepcion(e, $"Error al intentar borrar del drive el archivo '{files.First().Name}'");
				}
			}				
			else
				Log.Info($"No se eliminó nada porque había {files.Count} backups más antiguos.");
		}		
	}
}