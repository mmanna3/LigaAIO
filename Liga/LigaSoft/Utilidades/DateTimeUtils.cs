using System;

namespace LigaSoft.Utilidades
{
	public static class DateTimeUtils
	{
		private const string FormatoFecha4DigAnio = "dd-MM-yyyy";
		private const string FormatoFecha2DigAnio = "dd-MM-yy";

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