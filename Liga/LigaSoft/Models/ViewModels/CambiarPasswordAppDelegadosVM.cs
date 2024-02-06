using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class CambiarPasswordAppDelegadosVM
	{
		public string Usuario { get; set; }
		
		public string NuevoPassword { get; set; }
	}
}