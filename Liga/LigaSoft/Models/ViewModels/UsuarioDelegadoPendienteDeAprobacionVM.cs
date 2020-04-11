using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class UsuarioDelegadoPendienteDeAprobacionVM : ViewModelConId
	{
		[YKNRequired]
		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "El email no es válido.")]
		public string Email { get; set; }

		[YKNRequired]
		[StringLength(100, ErrorMessage = "La contraseña tiene que tener al menos 6 caracteres.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirmá la contraseña")]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
		public string ConfirmPassword { get; set; }

		public List<SelectListItem> ClubsParaCombo { get; set; }

		[YKNRequired, Display(Name = "Club")]
		public int ClubId { get; set; }

		public string Club { get; set; }

		[YKNRequired, Display(Name = "Nombre")]
		[StringLength(50, ErrorMessage = "El nombre debe tener al menos 3 caracteres.", MinimumLength = 3)]
		public string Nombre { get; set; }

		[YKNRequired, Display(Name = "Apellido")]
		[StringLength(50, ErrorMessage = "El nombre debe tener al menos 3 caracteres.", MinimumLength = 3)]
		public string Apellido { get; set; }
	}
}