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

				// and start it off
				await scheduler.Start();

				var job = JobBuilder.Create<SubirBackupAlDriveJob>()
					.WithIdentity("subirBackupAlDrive", "group1")
					.Build();

				// Trigger the job to run now, and then repeat every 120 seconds
				var trigger = TriggerBuilder.Create()
					.WithIdentity("trigger1", "group1")
					.StartAt(GetStartTime())
					.WithSimpleSchedule(x => x
						.WithIntervalInSeconds(86400)
						.RepeatForever())
					.Build();

				// Tell quartz to schedule the job using our trigger
				await scheduler.ScheduleJob(job, trigger);

				// some sleep to show what's happening
				//await Task.Delay(TimeSpan.FromSeconds(60));

				// and last shut down the scheduler when you are ready to close your program
				//await scheduler.Shutdown();
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

			Log.Info($"El backup se programó para: {dateTime.ToString(IODiskUtility.FormatoFechaBackup)}");

			return dateTime;
		}
	}
}