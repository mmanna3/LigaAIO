using System.Threading.Tasks;
using LigaSoft.Utilidades.Backup;
using Quartz;

namespace LigaSoft.Scheduler
{
	public class SubirBackupAlDriveJob : IJob
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public async Task Execute(IJobExecutionContext context)
		{
			Log.Info("QUARTZ: Comienza el job SubirBackupAlDrive");

			GoogleDriveBackupManager.GenerarBackupImagenes();

			GoogleDriveBackupManager.GenerarBackupBaseDeDatos();

			Log.Info("QUARTZ: Finaliza el job SubirBackupAlDrive");
		}
	}
}