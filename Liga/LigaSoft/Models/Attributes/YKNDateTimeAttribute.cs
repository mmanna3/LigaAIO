using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Attributes
{
	public class YKNDateTimeAttribute : RegularExpressionAttribute
	{
		public YKNDateTimeAttribute() : base(@"[0-9]{2}-[0-9]{2}-[0-9]{4}")
		{
			ErrorMessage = "La fecha no tiene el formato correcto.";
		}
	}
}