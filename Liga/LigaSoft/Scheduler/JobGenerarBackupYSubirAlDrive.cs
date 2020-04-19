using System.Threading.Tasks;
using LigaSoft.Utilidades;
using Quartz;

namespace LigaSoft.Scheduler
{
	public class JobGenerarBackupYSubirAlDrive : IJob
	{	
		#pragma warning disable 1998
		public async Task Execute(IJobExecutionContext context)
		{
			Log.Info("QUARTZ: Comienza el job GenerarBackupYSubirAlDrive");

			BackupBaseDeDatosYFileSystem.GenerarYSubirADrive();

			Log.Info("QUARTZ: Finaliza el job GenerarBackupYSubirAlDrive");
		}
	}
}