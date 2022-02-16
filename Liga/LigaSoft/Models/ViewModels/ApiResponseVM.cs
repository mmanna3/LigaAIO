namespace LigaSoft.Models.ViewModels
{
	public class ApiResponseVM
	{
#pragma warning disable IDE1006 // Naming Styles (por minúsculas al principio)
		public bool huboError { get; set; }
		public string mensajeDeError { get; set; }
		public object contenido { get; set; }
	}

	public static class ApiResponseCreator
	{
		public static ApiResponseVM Error(string mensaje)
		{
			return new ApiResponseVM
			{
				huboError = true,
				mensajeDeError = mensaje,
			};
		}

		public static ApiResponseVM Exito(object contenido)
		{
			return new ApiResponseVM
			{
				huboError = false,
				contenido = contenido,
			};
		}
	}
}