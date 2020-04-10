using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class DisplayFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;		
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private string _label;

		public DisplayFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		} 

		public DisplayFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='form-group'>
						{LabelTag(_expression, _label)}
						<div>{_helper.DisplayFor(_expression).ToHtmlString()}</div>
					</div>";
		}	
	}
}