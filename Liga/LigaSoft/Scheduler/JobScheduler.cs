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
						.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(18, 0)) //15 En Buenos Aires
						.InTimeZone(TimeZoneInfo.Utc))
					.Build();

				await scheduler.ScheduleJob(job, trigger);
				Log.Info("Se programó el backup.");	//Borrar después
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}
	}
}