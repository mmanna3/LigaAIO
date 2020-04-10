using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers
{
	public class DisplayMultilineFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;		
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private string _label;
		private readonly IEnumerable<string> _list;

		public DisplayMultilineFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_list = (IEnumerable<string>)ValueOfExpression(helper, expression);
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		}

		public DisplayMultilineFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='form-group'>
						{LabelTag(_expression, _label)}							
						<div>{ElementosDeLaListaFormateados()}</div>
					</div>";
		}

		private string ElementosDeLaListaFormateados()
		{
			var result = string.Empty;

			foreach (var item in _list)
				result+= $@"<div>{item}</div>";

			return result;
		}

	}
}