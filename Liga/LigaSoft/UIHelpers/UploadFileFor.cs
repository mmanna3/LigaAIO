using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace LigaSoft.UIHelpers
{
	public class UploadFileFor<TModel, TProperty> : UIBuilder
	{
		private string _label;
		private string _accepted = string.Empty;
		private readonly string _propertyName;
		private string _class = string.Empty;

		public UploadFileFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_label = DefaultLabel(helper, expression);
			_propertyName = PropertyName(expression);
		} 

		public UploadFileFor<TModel, TProperty> Label(string label)
		{
			_label = label;
			return this;
		}

		public UploadFileFor<TModel, TProperty> AcceptedExtension(string acceptedExtensions)
		{
			_accepted = $"accept='{acceptedExtensions}'";
			return this;
		}

		public override string ToHtmlString()
		{
			return $@"<div class='form-group'>
						<label class='btn btn-default {_class}'  for='{_propertyName}'>
							<input id ='{_propertyName}' name='{_propertyName}' onchange =""$('#upload-file-info').html('El archivo fue cargado correctamente')"" {_accepted} style='display:none' type ='file'>
				           {_label}
						</label >
						<span class=""label label-success"" id=""upload-file-info""></span>
					</div>";
		}

		public UploadFileFor<TModel, TProperty> Class(string classes)
		{
			_class = classes;
			return this;
		}
	}
}