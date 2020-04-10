using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers
{
	public class WebCamFor<TModel, TProperty> : UIBuilder
	{
		private readonly HtmlHelper<TModel> _helper;
		private readonly Expression<Func<TModel, TProperty>> _expression;

		public WebCamFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			_expression = expression;
			_helper = helper;
		}

		public override string ToHtmlString()
		{
			return $@"
				{ScriptString()}
				{_helper.HiddenFor(_expression, string.Empty)}
				<div class=""row"">				
					<div id=""camera"">
						<div class=""placeholder"">
							Preparando cámara...
						</div>
					</div>
				</div>
				{MensajeValidacionHtml(_helper, _expression)}
				<div class=""row"" style=""padding-top: 20px;"">
					<div class=""col-sm-6"">
						{_helper.YKN().Button("Capturar").OnClick("capturar()").Color(BootstrapColorEnum.Default).FullWidth().ToHtmlString()}
					</div>
					<div class=""col-sm-6"">
						{_helper.YKN().Button("Reiniciar").OnClick("reiniciar()").Color(BootstrapColorEnum.Default).FullWidth().ToHtmlString()}
					</div>
				</div>";
		}

		public string ScriptString()
		{
			return $@"<script>
						var camera;

						$(document).ready(function () {{
							camera = new JpegCamera(""#camera"").ready(function () {{}});		
						}});

						function capturar() {{
							var snapshot = camera.capture();
							snapshot.show();
							$('#Foto').val(snapshot._canvas.toDataURL());
						}};

						function reiniciar() {{
							camera.show_stream();
						}};
					</script>";
		}
	}
}