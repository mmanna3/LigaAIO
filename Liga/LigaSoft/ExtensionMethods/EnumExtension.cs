using System;
using System.ComponentModel.DataAnnotations;

namespace LigaSoft.ExtensionMethods
{
	public static class EnumExtension
	{
		public static string Descripcion(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attribs = field.GetCustomAttributes(typeof(DisplayAttribute), true);

			if (attribs.Length > 0)
				return ((DisplayAttribute)attribs[0]).Name;

			return value.ToString();
		}
	}
}