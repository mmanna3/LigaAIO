using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LigaSoft.Utilidades;
using Quartz;
using Quartz.Impl;

namespace LigaSoft.Scheduler
{
	public class JobScheduler
	{
		public static async Task Start()
		{
			try
			{
				var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
				await scheduler.Start();

				var job = JobBuilder.Create<SubirBackupAlDriveJob>()
					.WithIdentity("subirBackupAlDrive", "group1")
					.Build();

				var trigger = TriggerBuilder.Create()
					.WithIdentity("trigger1", "group1")
					.StartAt(GetStartTime())
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(86400)
						.RepeatForever())
					.Build();

				await scheduler.ScheduleJob(job, trigger);				
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}

		private static DateTimeOffset GetStartTime()
		{
			DateTime fechaHoraComienzaBackupBsAs;
			var fechaHoraActualEnBsAs = AhoraEnBuenosAires();

			if (fechaHoraActualEnBsAs.Hour > 4) //Si son más de las 4 de la mañana, agendarlo para mañana.
			{
				fechaHoraActualEnBsAs = fechaHoraActualEnBsAs.AddDays(1);
				fechaHoraComienzaBackupBsAs = new DateTime(fechaHoraActualEnBsAs.Year, fechaHoraActualEnBsAs.Month, fechaHoraActualEnBsAs.Day, 4, 0, 0);
			}				
			else
				fechaHoraComienzaBackupBsAs = new DateTime(fechaHoraActualEnBsAs.Year, fechaHoraActualEnBsAs.Month, fechaHoraActualEnBsAs.Day, 4, 0, 0);


			var result = new DateTimeOffset(fechaHoraComienzaBackupBsAs).ToUniversalTime();

			Log.Info($"Hora de comienzo de backup: - Buenos Aires: '{fechaHoraComienzaBackupBsAs.ToString(IODiskUtility.FormatoFechaBackup)}' - UTC: '{result.ToString(IODiskUtility.FormatoFechaBackup)}' ");

			return result;
		}

		private static DateTimeOffset AhoraEnBuenosAires()
		{
			return DateTimeOffset.UtcNow.ToOffset(new TimeSpan(-3, 0, 0));
		}
	}
}