using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class EditorFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private string _label;
		private bool _noLabel = false;
		private string _classes = string.Empty;
		private int _tabIndex;
		private string _onChangeJsFunc = "";

		public EditorFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
		}

		public EditorFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public EditorFor<TModel, TProperty> NoLabel()
		{
			_noLabel = true;
			return this;
		}

		public EditorFor<TModel, TProperty> Classes(string classes)
		{
			_classes = classes;
			return this;
		}

		public override string ToHtmlString()
		{
			var label = "";
			if (!_noLabel)
				label = LabelTag(_expression, _label);


			return $@"<div class='form-group'>				
						{label}
						{_helper.EditorFor(_expression, new { htmlAttributes = new { @class = $"form-control {_classes}", autocomplete = "off", tabindex = _tabIndex, onchange = _onChangeJsFunc } }).ToHtmlString()}
						{MensajeValidacionHtml(_helper, _expression)}
					</div>";
		}

		public EditorFor<TModel, TProperty> TabIndex(int tabIndex)
		{
			_tabIndex = tabIndex;
			return this;
		}

		public EditorFor<TModel, TProperty> Disabled()
		{
			_classes = "disabled-input";
			_tabIndex = -1;
			return this;
		}

		public EditorFor<TModel, TProperty> OnChange(string onChangeJsFunc)
		{
			_onChangeJsFunc = onChangeJsFunc;
			return this;
		}
	}
}