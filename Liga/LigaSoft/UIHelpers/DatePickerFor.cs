using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.Utilidades;

namespace LigaSoft.UIHelpers
{
	public class DatePickerFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private readonly string _expressionId;
		private string _defaultValue = "";
		private const string DateFormat = "dd-MM-yyyy";

		public DatePickerFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_expressionId = PropertyName(expression);
		}

		public DatePickerFor<TModel, TProperty> DefaultEsHoy()
		{
			_defaultValue = DateTime.Today.ToString(DateFormat);
			return this;
		}

	public override string ToHtmlString()
		{
			return $@"
			<div class=""form-group"">
				{_helper.LabelFor(_expression)}
				{_helper.TextBoxFor(_expression, new { id = _expressionId, @class = "form-control" })}
				{MensajeValidacionHtml(_helper, _expression)}
			</div>
			{ScriptString()}";
		}

		public string ScriptString()
		{
			return $@"<script>
						$(function () {{ // will trigger when the document is ready
								$('#{_expressionId}').datepicker({{
									uiLibrary: 'bootstrap',
									value: '{_defaultValue}',
									format: 'dd-mm-yyyy',
									locale: 'es-es'
								}});
							}});
					</script>";
		}

		/// <summary>
		/// Formato: dd-MM-yyyy
		/// </summary>
		public DatePickerFor<TModel, TProperty> DefaultValue(string defaultValue)
		{
			_defaultValue = defaultValue;			
			return this;
		}
	}
}