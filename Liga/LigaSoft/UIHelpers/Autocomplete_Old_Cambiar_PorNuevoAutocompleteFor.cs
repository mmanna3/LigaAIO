using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.UIHelpers
{
	public class Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private readonly string _expressionId;
		private readonly string _textBoxId;
		private string _label;
		private string _url;
		private readonly Dictionary<string, string> _dict;
		private string _source;
		private string _jsonSource;
		private int _defaultValue;
		private string _defaultDescription = "";
		private string _updaterJsFunc = "";

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_expressionId = PropertyName(expression);
			_textBoxId = _expressionId + "txtSearch";
			_label = _helper.LabelFor(_expression).ToString();
			_dict = new Dictionary<string, string>();
		}

		public override string ToHtmlString()
		{
			return $@"
		{_label}
		<div class=""input-group"">
			<span class=""input-group-addon"">
				<i class=""fa fa-search""></i>
			</span>
			{_helper.TextBox(_textBoxId, null, new { @class = "form-control", id = _textBoxId, autocomplete = "off" })}
			{MensajeValidacionHtml(_helper, _expression)}
		</div>
		{_helper.Hidden(_expressionId, "")}
		{ScriptString()}";
		}

		public string ScriptString()
		{
			_source = _url != null ? AjaxSource() : StaticSource(); //Es feo, pero ya fue.

			return $@"<script>
					$(function () {{
						$('#{_textBoxId}').typeahead({{
							hint: true,
							highlight: true,
							minLength: 1,
							source: function (request, response) {{
								{_source}
							}},
							updater: function (item) {{
								$('#{_expressionId}').val(window.map[item].id);
								{_updaterJsFunc};
								return item;
							}}
						}});
						$('#{_textBoxId}').val('{_defaultDescription}');
						$('#{_expressionId}').val({_defaultValue});
					}});	
				</script>";
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> Label(string value)
		{
			_label = _helper.Label(value).ToString();
			return this;
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> JsonSource(string json)
		{
			_jsonSource = json;
			return this;
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> Default(IdDescripcionVM defaults)
		{
			_defaultDescription = defaults.Descripcion;
			_defaultValue = defaults.Id;
			return this;
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> Url(string controller, string method)
		{
			var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			_url = url.Action(method, controller);
			return this;
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> AddParam(string key, string value)
		{
			_dict.Add(key, value);
			return this;
		}

		private string AdditionalParamsToJavascript()
		{
			var result = string.Empty;

			foreach (var entry in _dict)
				result += $"{entry.Key}: '{entry.Value}',";

			return result;
		}

		private string AjaxSource()
		{
			return $@"	$.ajax({{
						url: '{_url}',
						data: {{
							{AdditionalParamsToJavascript()}
							term: $('#{_textBoxId}').val()
						}},
						dataType: ""json"",
						success: function (data) {{
							var optionsArray = [];
							window.map = {{}};
							response($.map(data, function (item) {{
								optionsArray.push(item.Descripcion);
								window.map[item.Descripcion] = {{ id: item.Id, desc: item.Descripcion }};
							}}));
							response(optionsArray);
							$("".typeahead"").addClass(""autocomplete-typeahead"");
						}},
						error: function (response) {{
							alert(response.responseText);
						}},
						failure: function (response) {{
							alert(response.responseText);
						}}
					}});";
		}

		private string StaticSource()
		{
			return $@"	var optionsArray = [];
					window.map = {{}};
					//var data = [{{""Id"":1,""Descripcion"":""machin""}},{{""Id"":2,""Descripcion"":""truc""}}]
					var data = {_jsonSource}
					response($.map(data, function (item) {{
						optionsArray.push(item.Descripcion);							
						window.map[item.Descripcion] = {{ id: item.Id, desc: item.Descripcion }};						
					}}));
					response(optionsArray);
					$("".typeahead"").addClass(""autocomplete-typeahead"");
		";
		}

		public Autocomplete_Old_Cambiar_PorNuevoAutocompleteFor<TModel, TProperty> OnSelected(string jsFunc)
		{
			_updaterJsFunc = jsFunc;
			return this;
		}
	}
}