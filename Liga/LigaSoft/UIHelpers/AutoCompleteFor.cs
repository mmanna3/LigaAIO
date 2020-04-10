using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.WebPages;
using LigaSoft.Models;

namespace LigaSoft.UIHelpers
{
	public class AutocompleteFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private List<TextValueItem> _values = new List<TextValueItem>();
		private string _label;
		private string _defaultValue = "";
		private string _classes = string.Empty;
		private readonly string _expressionId;
		private string _onChangeFuncExecutor = string.Empty;
		private LoadValuesOnElementChange _loadValuesOnElementChange;

		public AutocompleteFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
			_expressionId = PropertyName(expression);
		}

		public AutocompleteFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public AutocompleteFor<TModel, TProperty> OnChange(string jsFunc)
		{
			_onChangeFuncExecutor = $"{jsFunc}();";
			return this;
		}

		public AutocompleteFor<TModel, TProperty> DefaultValue(object defaultValue)
		{
			_defaultValue = defaultValue.ToString();
			return this;
		}

		public AutocompleteFor<TModel, TProperty> LoadValuesOnElementChange(string elementToWatchId, string urlFromLoadingData, string elementToWatchParamNameInServer)
		{
			_loadValuesOnElementChange = new LoadValuesOnElementChange
			{
				ElementToWatchId = elementToWatchId,
				UrlForLoadingData = urlFromLoadingData,
				ElementToWatchParamNameInServer = elementToWatchParamNameInServer
			};
			return this;
		}

		public AutocompleteFor<TModel, TProperty> Values(List<TextValueItem> values)
		{
			_values = values;
			return this;
		}

		public AutocompleteFor<TModel, TProperty> Classes(string classes)
		{
			_classes = classes;
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"
						<div class='form-group'>				
						{LabelTag(_expression, _label)}				
						<select class='form-control {_classes}' name='{_expressionId}' id='{_expressionId}'>						
						{OptionsValues()}
						</select>	   
						</div>
						{MensajeValidacionHtml(_helper, _expression)}
						{ScriptString()}
					";
		}

		private string OptionsValues()
		{
			var result = "";
			foreach (var listItem in _values)
				result += $"<option value='{listItem.Value}'>{listItem.Text}</option>";

			return result;
		}

		private string ScriptString()
		{
			return $@"<script>
						$(function () {{
								$('#{_expressionId}').select2({{
									theme: ""bootstrap""
								}})
								.on(""change"", function (e) {{
									{_onChangeFuncExecutor}
								}});

								$('.select2.select2-container.select2-container--bootstrap').css('width', ''); //Porque se pone con width:83px y la verdad que no sé por qué
																
								{DefaultValueScript()}	//Si tiene LoadValuesOnElementChangeScript, esto lo va a hacer dos veces.
							}});

						{LoadValuesOnElementChangeScript()}
					</script>";
		}

		private string DefaultValueScript()
		{
			if (!_defaultValue.IsEmpty())
				return $@"		$('#{_expressionId}').val('{_defaultValue}');
								$('#{_expressionId}').trigger('change');
						";
			return "";
		}

		private string LoadValuesOnElementChangeScript()
		{
			if (_loadValuesOnElementChange == null)
				return "";

			return $@"	
						$('#{_loadValuesOnElementChange.ElementToWatchId}').on(""change"", function (e) {{
							loadValuesOnElementChange_{_expressionId}();
						}});
						
						function loadValuesOnElementChange_{_expressionId}() {{
								var elementToWatchValue = $('#{_loadValuesOnElementChange.ElementToWatchId}').val();

								$.ajax({{
									url: '{_loadValuesOnElementChange.UrlForLoadingData}',
									type: 'GET',
									data: {{ {_loadValuesOnElementChange.ElementToWatchParamNameInServer}: elementToWatchValue }}
								}})
								.done(function(data) {{
									$('#{_expressionId}').empty();

									data.forEach(function(item) {{
										$('#{_expressionId}').append($('<option />').val(item.Value).text(item.Text));
									}});

									{DefaultValueScript()}
								}});
						}}
						
						loadValuesOnElementChange_{_expressionId}();
					";
		}
	}

	public class LoadValuesOnElementChange
	{
		public string ElementToWatchId { get; set; }
		public string UrlForLoadingData { get; set; }
		public string ElementToWatchParamNameInServer { get; set; }
	}
}