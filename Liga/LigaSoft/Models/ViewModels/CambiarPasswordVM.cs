using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class CambiarPasswordVM
	{
		[Display(Name = "Password anterior")]
		[DataType(DataType.Password)]
		public string PasswordAnterior { get; set; }

		[Display(Name = "Nuevo password")]
		[DataType(DataType.Password)]
		[StringLength(20, ErrorMessage = "La contraseña debe tener entre 5 y 20 caracteres..", MinimumLength = 5)]
		public string PasswordNuevo { get; set; }

		[Display(Name = "Repetir nuevo password")]
		[DataType(DataType.Password)]
		[Compare("PasswordNuevo", ErrorMessage = "Las contraseñas no coinciden.")]
		public string PasswordNuevoRepetido { get; set; }
	}
}