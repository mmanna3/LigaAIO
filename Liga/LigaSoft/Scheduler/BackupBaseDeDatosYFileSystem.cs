﻿using System;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Backup;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Scheduler
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
				YKNExHandler.LoguearYLanzarExcepcion(e, "Error en el job SubirBackupAlDrive");
			}

			Log.Info("QUARTZ: Finaliza el job SubirBackupAlDrive");
			Log.Info("------------------------------------------------");
		}
	}
}