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
				var timeZoneInfoBsAs = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
				var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
				await scheduler.Start();

				var job = JobBuilder.Create<SubirBackupAlDriveJob>()
					.WithIdentity("subirBackupAlDrive", "group1")
					.Build();

				var trigger = TriggerBuilder.Create()
					.WithIdentity("trigger1", "group1")
					.WithDailyTimeIntervalSchedule(s => s
						.OnEveryDay()
						.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(5, 0))
						.InTimeZone(timeZoneInfoBsAs))
					.Build();

				var horaProximoBackup = await scheduler.ScheduleJob(job, trigger);

				Log.Info($"Hora próximo backup: {horaProximoBackup:dd/MM/yyyy HH:mm} (UTC). {TimeZoneInfo.ConvertTime(horaProximoBackup, timeZoneInfoBsAs):dd/MM/yyyy HH:mm} (Buenos Aires).");
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}
	}
}