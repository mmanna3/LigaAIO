using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class SiNoFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private string _label;
		private string _classes = string.Empty;

		public SiNoFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		}

		public SiNoFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public SiNoFor<TModel, TProperty> Classes(string classes)
		{
			_classes = classes;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='form-group'>
						{LabelTag(_expression, _label)}
						<div class='form-check form-check-inline' style='margin-top:6px;'>
							<label class='radio-inline'>						
								{_helper.RadioButtonFor(_expression, true)}Sí								
							</label>
							<label class='radio-inline'>						
								{_helper.RadioButtonFor(_expression, false)}No
							</label>
						</div>
						{MensajeValidacionHtml(_helper, _expression)}
					</div>";
		}
	}
}