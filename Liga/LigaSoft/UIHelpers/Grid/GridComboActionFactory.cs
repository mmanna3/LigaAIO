using System.Collections.Generic;
using System.Linq;

namespace LigaSoft.UIHelpers.Grid
{
	public class GridComboActionFactory
	{
		public List<GridComboAction> ComboActionOfTheGrid { get; set; }

		public GridComboActionFactory()
		{
			ComboActionOfTheGrid = new List<GridComboAction>();
		}

		public GridComboAction RedirectWithRowId(string label, string controller, string method)
		{
			var action = new GridComboAction(label, controller, method);
			ComboActionOfTheGrid.Add(action);
			return action;
		}

		public GridComboAction RedirectWithRowId(string label, string controller, string method, bool habilitar)
		{
			if (habilitar)
			{
				var action = new GridComboAction(label, controller, method);
				ComboActionOfTheGrid.Add(action);
				return action;
			}
			return new GridComboAction(label, controller, method);
		}


		public GridComboAction RedirectParentWithChildWithRowId(string label, object routeValues)
		{
			var action = new GridComboAction(label, routeValues);
			ComboActionOfTheGrid.Add(action);
			return action;
		}

		public GridComboAction RedirectWithRowIdAsParent(string label, string parentName, string controller, string method)
		{
			var action = new GridComboAction(label, parentName, controller, method);
			ComboActionOfTheGrid.Add(action);
			return action;
		}

		public GridComboAction JavaScriptWithRowId(string label, string jsFuncName)
		{
			var action = new GridComboAction(label, jsFuncName);
			ComboActionOfTheGrid.Add(action);
			return action;
		}

		public string ToJs()
		{
			return ComboActionOfTheGrid.Any() ? $@"{{ tmpl: '<div class=""dropdown""><button class=""btn btn-default dropdown-toggle"" type=""button"" id=""menu1"" data-toggle=""dropdown"">Acciones<span class=""caret""></span></button><ul class=""dropdown-menu"" role=""menu"" aria-labelledby=""menu1"">{string.Join(" ", ComboActionOfTheGrid.Select(x => x.ToHtmlString()))}</ul></div>' }}" : string.Empty;
		}

		public string JavaScriptToAppend()
		{
			return $@"	function redirectToDataUrlUsingDataIdAsParam(obj) {{
							window.location.href = obj.getAttribute('data-url') + obj.getAttribute('data-id');
						}}

						function redirectToParentDataUrlUsingDataIdAsParam(obj) {{
							window.location.href = obj.getAttribute('data-parent') + obj.getAttribute('data-id') + obj.getAttribute('data-url');
						}}";
		}
	}
}