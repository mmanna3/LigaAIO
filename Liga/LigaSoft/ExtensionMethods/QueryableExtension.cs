using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.Models.Interfaces;

namespace LigaSoft.ExtensionMethods
{
    public static class QueryableExtension
    {
	    // ReSharper disable PossibleNullReferenceException porque si implementa interfaz, la property no es null 
		public static List<SelectListItem> ToComboValues<T>(this IQueryable<T> query)
			where T : class, new()
		{			
			try
			{
				var clase = new T() as IClassConIdDescripcion;
				var list = new List<SelectListItem>();

				var propId = clase.GetType().GetProperty("Id");
				var propDescripcion = clase.GetType().GetProperty("Descripcion");

				foreach (var obj in query)
				{
					var item = new SelectListItem
					{
						Value = (propId.GetValue(obj, null) is int ? (int)propId.GetValue(obj, null) : 0).ToString(),
						Text = propDescripcion.GetValue(obj, null) as string
					};

					list.Add(item);
				}

				return list;
			}
			catch (Exception)
			{
				throw new Exception("El objeto no implementa IClassConIdDescripcion");
			}			
		}

	    public static List<SelectListItem> ToComboValuesAgregandoBlancoAlPrincipio<T>(this IQueryable<T> query)
		    where T : class, new()
	    {
		    try
		    {
			    var clase = new T() as IClassConIdDescripcion;
			    var list = new List<SelectListItem> {new SelectListItem{Selected = true}};

			    var propId = clase.GetType().GetProperty("Id");
			    var propDescripcion = clase.GetType().GetProperty("Descripcion");

			    foreach (var obj in query)
			    {
				    var item = new SelectListItem
				    {
					    Value = (propId.GetValue(obj, null) is int ? (int)propId.GetValue(obj, null) : 0).ToString(),
					    Text = propDescripcion.GetValue(obj, null) as string
				    };

				    list.Add(item);
			    }

			    return list;
		    }
		    catch (Exception)
		    {
			    throw new Exception("El objeto no implementa IClassConIdDescripcion");
		    }
	    }
	}
}