using System.ComponentModel.DataAnnotations;
using System.Web;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class JugadorFichadoPorDelegadoVM : ViewModelConId
	{		
		[YKNRequired, YKNStringLength(Maximo = 9), RegularExpression(@"^[0-9]*$", ErrorMessage = "El DNI sólo puede contener números")]
		public string DNI { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Nombre { get; set; }

		[YKNRequired, YKNStringLength(Maximo = 14)]
		public string Apellido { get; set; }

		[YKNRequired, YKNDateTime, Display(Name= "Fecha nacimiento")]
		public string FechaNacimiento { get; set; }

		public int EquipoId { get; set; }

		public string Equipo { get; set; }

		public string Club { get; set; }

		public EstadoJugadorFichadoPorDelegado Estado { get; set; }

		[Display(Name = "Motivo de rechazo")]
		public string MotivoDeRechazo { get; set; }

		public HttpPostedFileBase FotoDNIFrente { get; set; }

		public string FotoCarnet { get; set; }

		public string FotoCarnetRelativePath { get; set; }

		public string FotoDNIFrenteRelativePath { get; set; }
	}
}