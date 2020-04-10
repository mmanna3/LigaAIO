using System;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.ExtensionMethods;

namespace LigaSoft.UIHelpers
{
	public class BuscadorFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private readonly string _placeHolder;
		private readonly string _textboxId;
		private readonly string _buttonId;
		private readonly string _searchField; //todo que sea un attribute del vm
		private readonly string _jsFunc;

		public BuscadorFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_placeHolder = DefaultLabel(helper, expression);
			_textboxId = PropertyName(expression);
			_buttonId = _textboxId + "btn";
			_searchField = _textboxId;  
			_jsFunc = $@"grid.reload({{ page: 1, searchField: ""{_searchField}"", searchValue: $('#{_textboxId}').val() }});";			
		}

		public override string ToHtmlString()
		{
			return $@"	{OnEnterSearch()}
						<div class='form-group'>
							{_helper.TextBoxFor(_expression, new {id = _textboxId, autocomplete = "off", @class = "form-control buscador-textbox", placeholder = _placeHolder })
									.ToHtmlString()
							} 
						
							{_helper.YKN().Button(_buttonId)
									.Classes("pull-left")
									.Label("Buscar")
									.OnClick(_jsFunc)
									.ToHtmlString()
							}
						</div>";
		}

		private string OnEnterSearch()
		{
			return $@"<script>
						$(function () {{
							$('#{_textboxId}').keydown(function (event) {{
									var keypressed = event.keyCode || event.which;
									if (keypressed == 13) {{
										$('#{_buttonId}').trigger('click');
									}}
								}});
						}});
					</script>";
		}
	}
}