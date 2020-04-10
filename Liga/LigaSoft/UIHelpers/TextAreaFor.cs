using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class TextAreaFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private string _label;
		private string _classes = string.Empty;
		private int _tabIndex;
		private string _onChangeJsFunc = "";

		public TextAreaFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		}

		public TextAreaFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public TextAreaFor<TModel, TProperty> Classes(string classes)
		{
			_classes = classes;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='form-group'>
						{LabelTag(_expression, _label)}
						{_helper.TextAreaFor(_expression, new { @class = $"form-control {_classes}", autocomplete = "off", tabindex = _tabIndex, onchange = _onChangeJsFunc } ).ToHtmlString()}
						{MensajeValidacionHtml(_helper, _expression)}
					</div>";
		}

		public TextAreaFor<TModel, TProperty> TabIndex(int tabIndex)
		{
			_tabIndex = tabIndex;
			return this;
		}

		public TextAreaFor<TModel, TProperty> Disabled()
		{
			_classes = "disabled-input";
			_tabIndex = -1;
			return this;
		}

		public TextAreaFor<TModel, TProperty> OnChange(string onChangeJsFunc)
		{
			_onChangeJsFunc = onChangeJsFunc;
			return this;
		}
	}
}