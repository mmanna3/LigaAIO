using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.Attributes
{
	public class YKNRangeAttribute : ValidationAttribute
	{
		public int Minimo { get; set; }
		public int Maximo { get; set; }

		public YKNRangeAttribute()
		{
			Minimo = 0;
			Maximo = int.MaxValue;			
		}

		public override bool IsValid(object value)
		{
			var intValue = value as int? ?? 0;			

			if (intValue < Minimo)
			{
				ErrorMessage = $@"El valor mínimo para el campo {{0}} es {Minimo}.";
				return false;
			}


			if (intValue > Maximo)
			{
				ErrorMessage = $@"El valor máximo para el campo {{0}} es {Maximo}.";
				return false;
			}				

			return true;
		}
	}
}