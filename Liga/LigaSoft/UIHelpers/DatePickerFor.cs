using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class DatePickerFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private readonly string _expressionId;
		private string _label;
		private string _defaultValue = "";
		private const string DateFormat = "dd-mm-yyyy";

		public DatePickerFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_expressionId = PropertyName(expression);
			_label = DefaultLabel(helper, expression);
		}

		public DatePickerFor<TModel, TProperty> Label(string value)
		{
			_label = value;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"
			<div class=""form-group"">
				{LabelTag(_expression, _label)}
				{_helper.TextBoxFor(_expression, new { id = _expressionId, @class = "form-control" })}
				{MensajeValidacionHtml(_helper, _expression)}
			</div>
			{ScriptString()}";
		}

		public string ScriptString()
		{
			return $@"<script>
						$(function () {{
								
								function valor() {{									
									let valorParaEdicion = $('#{_expressionId}').attr('value');
									if (valorParaEdicion)
										return valorParaEdicion;
									return '{_defaultValue}';
								}}

								$('#{_expressionId}').datepicker({{
									uiLibrary: 'bootstrap',
									value: valor(),
									format: '{DateFormat}',
									locale: 'es-es'
								}});
								
								$('#{_expressionId}').focusin(function () {{
									var dataGuid = $('#{_expressionId}').attr('data-guid');
									if ($(`*[guid=${{dataGuid}}]`).css('display') === 'none') {{
										$('#{_expressionId}').datepicker().show();
									}}
								}});

								$('#{_expressionId}').closest('.gj-datepicker').css(""width"", ""100%"");
							}});
					</script>";
		}

		public DatePickerFor<TModel, TProperty> DefaultEsHoy()
		{
			_defaultValue = DateTime.Today.ToString("dd-MM-yyyy");
			return this;
		}

		public DatePickerFor<TModel, TProperty> DefaultValue(string valor)
		{
			_defaultValue = valor;
			return this;
		}
	}
}