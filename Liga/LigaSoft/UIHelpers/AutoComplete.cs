using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.UIHelpers
{
	public class AutoComplete<TModel> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly string _id;
		private readonly string _textBoxId;
		private string _label;
		private string _url;
		private readonly Dictionary<string, string> _dict;
		private string _source;
		private string _jsonSource;
		private string _hiddenId;
		private int _defaultValue;
		private string _defaultDescription = "";

		public AutoComplete(HtmlHelper<TModel> helper, string id)
		{
			_id = id;
			_helper = helper;			
			_hiddenId = id;
			_textBoxId = id + "txtSearch";
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
			</div>
			{_helper.Hidden(_hiddenId, "")}

			{ScriptString()}";
		}

		public string ScriptString()
		{
			_source = _url != null ? AjaxSource() : StaticSource();	//Es feo, pero ya fue.

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
								$(""input[name = '{_hiddenId}']"").val(window.map[item].id);
								return item;
							}}
						}});
							
						$('#{_textBoxId}').val('{_defaultDescription}');
						$(""input[name = '{_hiddenId}']"").val({_defaultValue});
					}});	

					</script>";
		}

		public AutoComplete<TModel> Label(string value)
		{
			_label = _helper.Label(value).ToString();
			return this;
		}

		public AutoComplete<TModel> HiddenId(string value)
		{
			_hiddenId = value;
			return this;
		}

		public AutoComplete<TModel> JsonSource(string json)
		{
			_jsonSource = json;
			return this;
		}

		public AutoComplete<TModel> Default(IdDescripcionVM defaults)
		{
			_defaultDescription = defaults.Descripcion;
			_defaultValue = defaults.Id;
			return this;
		}

		public AutoComplete<TModel> Url(string controller, string method)
		{
			var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
			_url = url.Action(method, controller);
			return this;
		}

		public AutoComplete<TModel> AddParam(string key, string value)
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
	}
}