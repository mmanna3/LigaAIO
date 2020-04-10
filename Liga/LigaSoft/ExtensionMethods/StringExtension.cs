using System;

namespace LigaSoft.ExtensionMethods
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string value)
        {
	        if (!string.IsNullOrEmpty(value) && value.Length > 1)
		        return char.ToUpperInvariant(value[0]) + value.Substring(1).ToLowerInvariant();
	        return value;
		}
	}
}