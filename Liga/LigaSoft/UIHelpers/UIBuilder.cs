using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
    public abstract class UIBuilder : IHtmlString
    {
        public abstract string ToHtmlString();

		protected static MvcHtmlString MensajeValidacionHtml<TModel, TProperty>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });
		}

	    protected static object ValueOfExpression<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
		    return ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
	    }

		protected static string DisplayNameAttribute<TModel, TProperty>(HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
	    {
		    var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
		    var name = metadata.DisplayName;
		    return name;
	    }

		protected static string PropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> prop)
	    {
		    var body = (MemberExpression)prop.Body;
		    return body.Member.Name;
	    }

	    public static string PropertyName<TModel>(Expression<Func<TModel, object>> exp)
	    {
		    if (!(exp.Body is MemberExpression body))
		    {
			    var ubody = (UnaryExpression)exp.Body;
			    body = ubody.Operand as MemberExpression;
		    }

		    return body.Member.Name;
	    }

		protected static string LabelTag<TModel, TProperty>(Expression<Func<TModel, TProperty>> prop, string labelText)
	    {
		    return $"<label for='{PropertyName(prop)}'>{labelText}</label>";
	    }

	    protected static string DefaultLabel<TModel, TProperty>(HtmlHelper<TModel> helperFor, Expression<Func<TModel, TProperty>> expression)
	    {
		    return DisplayNameAttribute(helperFor, expression) ?? PropertyName(expression);
	    }

	    protected static string FormatearParametrosParaUrl(Dictionary<string, string> paramDictionary)
	    {
		    var parameters = string.Empty;
		    if (paramDictionary != null)
		    {
			    parameters = $"/?{paramDictionary.First().Key}={paramDictionary.First().Value}";

			    foreach (var param in paramDictionary.Skip(1))
				    parameters += $"&{param.Key}={param.Value}";
		    }
		    return parameters;
	    }
	}
}