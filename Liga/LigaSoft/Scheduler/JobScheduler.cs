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
						.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(21, 30))
						.InTimeZone(TimeZoneInfo.Utc))
					.Build();

				var horaProximoBackup = await scheduler.ScheduleJob(job, trigger);
				Log.Info($@"Se programó el próximo backup para:
								-UTC: {horaProximoBackup:dd/MM/yyyy HH:mm}
								-Servidor: {horaProximoBackup.ToLocalTime():dd/MM/yyyy HH:mm}
								-Bs. As.: {horaProximoBackup.ToOffset(new TimeSpan(-3, 0, 0)):dd/MM/yyyy HH:mm}
						");
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}
	}
}