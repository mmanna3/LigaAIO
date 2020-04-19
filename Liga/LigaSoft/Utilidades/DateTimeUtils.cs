using System;

namespace LigaSoft.Utilidades
{
	public static class DateTimeUtils
	{
		private const string FormatoFecha4DigAnio = "dd-MM-yyyy";
		public const string FormatoFechaBackup = "yyyy-MM-dd--HH-mm-ss";

		public static readonly TimeZoneInfo timeZoneInfoArg = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
		public static string NowInArgentinaWithMiliseconds = $"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfoArg):dd/MM/yyyy HH:mm:ss.fff}";
		public static string NowInArgentinaBackupFormat = $"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfoArg).ToString(FormatoFechaBackup)}";

		public static DateTime ConvertToDateTime(string value, string formato = FormatoFecha4DigAnio)
		{
			return DateTime.ParseExact(value, formato, System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ConvertToString(DateTime value)
		{
			return value.ToString(FormatoFecha4DigAnio);
		}
	}
}