using System;
using System.Threading.Tasks;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Utilidades.Backup
{
	public static class BackupBaseDeDatosYFileSystem
	{		
		public static async Task GenerarYSubirADrive()
		{
			Log.Info("------------------------------------------------");

			try
			{
				await new ImagenesGDriveBackupManager().GenerarYSubirAlDrive();
				await new BaseDeDatosGDriveBackupManager().GenerarYSubirAlDrive();
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