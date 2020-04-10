namespace LigaSoft.ExtensionMethods
{
    public static class BoolExtension
    {
        public static string ToJavaScript(this bool value)
        {
	        return value.ToString().ToLower();
        }

	    public static string ToSiNoString(this bool value)
	    {
		    return value ? "Sí" : "No";
	    }

	    public static string ToCheckString(this bool value)
	    {
		    return value ? "✓" : "X";
	    }
	}
}