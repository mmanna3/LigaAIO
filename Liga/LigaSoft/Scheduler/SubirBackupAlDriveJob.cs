using System;
using System.Threading.Tasks;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Backup;
using Quartz;

namespace LigaSoft.Scheduler
{
	public class SubirBackupAlDriveJob : IJob
	{
		#pragma warning disable 1998
		public async Task Execute(IJobExecutionContext context)
		{
			Log.Info("------------------------------------------------");
			Log.Info("QUARTZ: Comienza el job SubirBackupAlDrive");

			try
			{
				new ImagenesGDriveBackupManager().GenerarYSubirAlDrive();
				new BaseDeDatosGDriveBackupManager().GenerarYSubirAlDrive();
				IODiskUtility.EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups();
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