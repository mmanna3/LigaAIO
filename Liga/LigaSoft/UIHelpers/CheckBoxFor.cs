using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class CheckBoxFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;		
		private readonly Expression<Func<TModel, bool>> _expression;
		private string _label;
		private string _chequeadoPorDefecto = "";
		private string _propertyName;

		public CheckBoxFor(HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
		{
			_expression = expression;
			_propertyName = PropertyName(expression);
			_helper = helper;
			_label = DefaultLabel(helper, expression);

		} 

		public CheckBoxFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public CheckBoxFor<TModel, TProperty> ChequeadoPorDefecto()
		{
			_chequeadoPorDefecto = "checked";
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"							
						<div><label for='{_propertyName}'>{_label}</label></div>
						<div><input type='checkbox' {_chequeadoPorDefecto} data-toggle='toggle' name='{_propertyName}' id='{_propertyName}' data-on='Sí' data-off='No'></div>
  
						{ScriptString()}
					";
		}

		public string ScriptString()
		{
			return $@"<link href='../../Content/bootstrap-toggle.min.css' rel='stylesheet'>
					<script src='../../Scripts/bootstrap-toggle.min.js'></script>

					<script>
						$(function() {{
							//Lo ejecuto una vez por si viene chequeado por defecto
							$('#{_propertyName}').attr('value', $('#{_propertyName}').prop('checked'));

							$('#{_propertyName}').change(function() {{
								$('#{_propertyName}').attr('value', $('#{_propertyName}').prop('checked'));
							}});
						}});
					</script>
				";
		}
	}
}