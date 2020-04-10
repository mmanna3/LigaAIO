using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LigaSoft.UIHelpers
{
	public class BrowseFileFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;
		private readonly string _label;
		private readonly string _expressionId;

		public BrowseFileFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
			_label = DefaultLabel(helper, expression);
			_expressionId = PropertyName(expression);
		}

		public override string ToHtmlString()
		{
			var id = _expressionId;
			var div = new TagBuilder("div");

			var label = new TagBuilder("label");
			label.AddCssClass("btn btn-default");
			label.MergeAttribute("for", id);

			var input = new TagBuilder("input");
			input.MergeAttribute("id", id);
			input.MergeAttribute("name", _expressionId);
			input.MergeAttribute("type", "file");
			input.MergeAttribute("style", "display:none");
			input.MergeAttribute("onchange", $"$(\'#upload-file-info\').html(this.files[0].name)");

			label.InnerHtml = input.ToString(TagRenderMode.SelfClosing) + _label;

			var span = new TagBuilder("span");
			span.AddCssClass("label label-info");
			span.MergeAttribute("id", $"upload-file-info");


			div.InnerHtml = label.ToString(TagRenderMode.Normal) + span.ToString(TagRenderMode.Normal);
			var result = div.ToString(TagRenderMode.Normal);

			return result;
		}
	}
}