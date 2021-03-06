﻿using System;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Utilidades.Backup
{
	public static class BackupBaseDeDatosYFileSystem
	{		
		public static void GenerarYSubirADrive()
		{
			Log.Info("------------------------------------------------");

			try
			{
				new ImagenesGDriveBackupManager().GenerarYSubirAlDrive();
				new BaseDeDatosGDriveBackupManager().GenerarYSubirAlDrive();
				new BackupDiskPersistence(new AppPathsWebApp()).EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups();
			}
			catch (Exception e)
			{
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error subiendo backup al Drive");
			}

			Log.Info("Finaliza la subida de backups al Drive");
			Log.Info("------------------------------------------------");
		}
	}
}