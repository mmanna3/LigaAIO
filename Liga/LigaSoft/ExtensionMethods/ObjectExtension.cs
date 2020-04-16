using System.Reflection;
using Google.Apis.Util;

namespace LigaSoft.ExtensionMethods
{
    public static class Object
    {
	    public static object GetReflectedProperty(this object obj, string propertyName)
	    {
		    obj.ThrowIfNull("obj");
		    propertyName.ThrowIfNull("propertyName");

		    PropertyInfo property = obj.GetType().GetProperty(propertyName);

		    if (property == null)
		    {
			    return null;
		    }

		    return property.GetValue(obj, null);
	    }
	}
}