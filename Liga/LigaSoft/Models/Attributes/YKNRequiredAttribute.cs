using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Attributes
{
	public class YKNRequiredAttribute : RequiredAttribute
	{
		public YKNRequiredAttribute()
		{
			ErrorMessage = "El campo {0} es requerido.";
		}
	}
}