using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.UIHelpers
{
	public class ComboFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private List<SelectListItem> _values;
		private string _label;
		private string _classes = string.Empty;		

		public ComboFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		}

		public ComboFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public ComboFor<TModel, TProperty> Values(List<SelectListItem> values)
		{
			_values = values;
			return this;
		}

		public ComboFor<TModel, TProperty> Values<TEnum>()
		{
			var enumValues = Enum.GetValues(typeof(TEnum)).Cast<Enum>();
			var list = new List<SelectListItem>();

			foreach (var enumVal in enumValues)
			{
				var item = new SelectListItem
				{
					Value = enumVal.ToString(),
					Text = enumVal.Descripcion()
				};

				list.Add(item);
			}

			_values = list;
			return this;
		}

		public ComboFor<TModel, TProperty> Classes(string classes)
		{
			_classes = classes;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"
						<div class='form-group'>
							{LabelTag(_expression, _label)}
							{_helper.DropDownListFor(_expression, _values, new { @class = $"form-control {_classes}"})}
							{MensajeValidacionHtml(_helper, _expression)}
						</div>
					";
		}
	}
}