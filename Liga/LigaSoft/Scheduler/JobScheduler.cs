using System;
using System.Threading.Tasks;
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
					.WithDailyTimeIntervalSchedule(s => s
						.WithIntervalInHours(24)
						.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0))
						.InTimeZone(TimeZoneInfo.Utc))
					.Build();

				var horaProximoBackup = await scheduler.ScheduleJob(job, trigger);

				Log.Info($"Hora próximo backup: (UTC) {horaProximoBackup:dd/MM/yyyy HH:mm}.");
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}
	}
}