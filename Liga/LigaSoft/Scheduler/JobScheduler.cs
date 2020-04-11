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
				var startTime = GetStartTime();
				var startTimeBuenosAires = startTime.ToUniversalTime().ToOffset(new TimeSpan(-3, 0, 0));

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
				Log.Info($"El backup se programó para: '{startTime.ToString(IODiskUtility.FormatoFechaBackup)}' (Buenos Aires: '{startTimeBuenosAires.ToString(IODiskUtility.FormatoFechaBackup)}')");
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}

		private static DateTimeOffset GetStartTime()
		{
			var dateTime = DateTimeOffset.Now;

			while (dateTime.Hour != 4)
				dateTime = dateTime.AddHours(1);

			return dateTime;
		}
	}
}