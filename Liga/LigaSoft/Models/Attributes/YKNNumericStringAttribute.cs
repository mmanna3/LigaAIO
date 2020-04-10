using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Attributes
{
	public class YKNNumericStringAttribute : RegularExpressionAttribute
	{
		public YKNNumericStringAttribute() : base(@"^[0-9]*$")
		{
			ErrorMessage = "Este campo es numérico.";
		}
	}
}