﻿using System;
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
					.WithDailyTimeIntervalSchedule
					(s =>
						s.WithIntervalInHours(24)
							.OnEveryDay()
							.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(5, 0))
							.InTimeZone(DateTimeUtils.timeZoneInfoArg)
					)
					.Build();

				var horaProximoBackup = await scheduler.ScheduleJob(job, trigger);

				Log.Info($"Hora próximo backup: {TimeZoneInfo.ConvertTime(horaProximoBackup, DateTimeUtils.timeZoneInfoArg):dd/MM/yyyy HH:mm} (Arg).");
			}
			catch (SchedulerException se)
			{
				Console.WriteLine(se);
			}
		}
	}
}