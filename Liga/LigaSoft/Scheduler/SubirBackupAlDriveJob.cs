using System.Threading.Tasks;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Backup;
using Quartz;

namespace LigaSoft.Scheduler
{
	public class SubirBackupAlDriveJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			Log.Info("QUARTZ: Comienza el job SubirBackupAlDrive");

			GoogleDriveBackupManager.GenerarBackupImagenes();

			GoogleDriveBackupManager.GenerarBackupBaseDeDatos();

			Log.Info("QUARTZ: Finaliza el job SubirBackupAlDrive");
		}
	}
}