using System;

namespace LigaSoft.Utilidades
{
	public static class YKNExHandler
	{
		public static void LoguearYLanzarExcepcion(Exception ex, string mensaje)
		{
			var mensajeError = $"{mensaje}. Excepción: {ex.Message}";
			Log.Error(mensajeError);
			throw new Exception(mensajeError);
		}
	}
}