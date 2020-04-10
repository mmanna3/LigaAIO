using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public abstract class ViewModelConId
	{
		[Display(Name = "Código")]
		public int Id { get; set; }
	}
}