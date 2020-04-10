using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Enums;

namespace LigaSoft.UIHelpers
{
	public class Form : IDisposable
	{
		private readonly TextWriter _writer;
		private readonly string _maxWidth = "";
		private readonly string _urlToPostTo;
		private readonly UrlHelper _url = new UrlHelper(HttpContext.Current.Request.RequestContext);
		private readonly HtmlHelper _helper;

		public Form(HtmlHelper helper, FormSizeEnum maxWidth = FormSizeEnum.Small)
		{
			_writer = helper.ViewContext.Writer;
			_helper = helper;

			if (maxWidth != FormSizeEnum.None)
				_maxWidth = $"style='max-width:{(int)maxWidth}px;'";

			_urlToPostTo = HttpContext.Current.Request.Url.AbsolutePath;

			WriteBeginFormTag();
		}

		public Form(HtmlHelper helper, string method, string controller, FormSizeEnum maxWidth = FormSizeEnum.Small)
		{
			_writer = helper.ViewContext.Writer;
			_helper = helper;
			_maxWidth = $"{(int)maxWidth}px";

			_urlToPostTo = _url.Action(method, controller);

			WriteBeginFormTag();
		}

		private void WriteBeginFormTag()
		{			
			_writer.Write($@"<form method='post' id='elForm' enctype='multipart/form-data' autocomplete='off' action='{_urlToPostTo}' {_maxWidth}>");
		}

		public void Dispose()
		{
			_writer.Write("</form>");
		}

	}
}