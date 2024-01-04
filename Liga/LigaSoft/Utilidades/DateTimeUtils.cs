using System;

namespace LigaSoft.Utilidades
{
	public static class DateTimeUtils
	{
		private const string FormatoFecha4DigAnio = "dd-MM-yyyy";
		private const string FormatoFecha2DigAnio = "dd-MM-yy";
		public const string FormatoFechaBackup = "yyyy-MM-dd--HH-mm-ss";
		
		public static readonly string NowInArgentinaWithMiliseconds = $"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfoArg()):dd/MM/yyyy HH:mm:ss.fff}";
		public static string NowInArgentinaBackupFormat = $"{TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfoArg()).ToString(FormatoFechaBackup)}";


		private static TimeZoneInfo TimeZoneInfoArg()
		{
			var p = (int) Environment.OSVersion.Platform;
			
			if ((p == 4) || (p == 6) || (p == 128)) {
				// es Unix
				return TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires");
			} 
			
			// Fallback es Windows
			return TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
		}
		
		public static DateTime ConvertToDateTime(string value, string formato = FormatoFecha4DigAnio)
		{
			return DateTime.ParseExact(value, formato, System.Globalization.CultureInfo.InvariantCulture);
		}

		public static string ConvertToString(DateTime value)
		{
			return value.ToString(FormatoFecha4DigAnio);
		}

		public static string ConvertToStringDdMmYy(DateTime value)
		{
			return value.ToString(FormatoFecha2DigAnio);
		}
	}
}