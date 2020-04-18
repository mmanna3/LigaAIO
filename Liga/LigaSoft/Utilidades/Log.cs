using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace LigaSoft.Utilidades
{
	public class Log
	{
		private static readonly Log Instance = new Log();
		protected ILog MonitoringLogger;
		protected static ILog DebugLogger;

		private Log()
		{
			MonitoringLogger = LogManager.GetLogger("MonitoringLogger");
			DebugLogger = LogManager.GetLogger("DebugLogger");
		}


		/// <summary>  
		/// Used to log Debug messages in an explicit Debug Logger  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Debug(string message)
		{
			DebugLogger.Debug(message);
		}


		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Debug(string message, System.Exception exception)
		{
			DebugLogger.Debug(message, exception);
		}


		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Info(string message)
		{
			System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
			var timeZoneInfoArg = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
			var horaUtc = DateTime.UtcNow.ToLocalTime().ToUniversalTime();
			var horaArg = TimeZoneInfo.ConvertTimeFromUtc(horaUtc, timeZoneInfoArg);

			var horaUtcYArg = $"{horaUtc:dd/MM/yy HH:mm:ss.fff} (UTC) - {horaArg:dd/MM/yyyy HH:mm:ss.fff} (Arg)";
			var mensajeConHoraBuenosAires = $"{horaUtcYArg} - {message}";		

			Instance.MonitoringLogger.Info(mensajeConHoraBuenosAires);
		}


		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Info(string message, System.Exception exception)
		{
			Instance.MonitoringLogger.Info(message, exception);
		}

		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Warn(string message)
		{
			Instance.MonitoringLogger.Warn(message);
		}

		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Warn(string message, System.Exception exception)
		{
			Instance.MonitoringLogger.Warn(message, exception);
		}

		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Error(string message)
		{
			Instance.MonitoringLogger.Error(message);
		}

		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Error(string message, System.Exception exception)
		{
			Instance.MonitoringLogger.Error(message, exception);
		}


		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Fatal(string message)
		{
			Instance.MonitoringLogger.Fatal(message);
		}

		/// <summary>  
		///  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Fatal(string message, System.Exception exception)
		{
			Instance.MonitoringLogger.Fatal(message, exception);
		}


	}
}