using System;
using System.IO;
using System.Web;

namespace Tests.Integration.Utilidades
{
	internal class HttpPostedFileRandomJpg : HttpPostedFileBase
	{
		private readonly Stream stream;

		private static MemoryStream GetRandomStream()
		{
			var byteBuffer = new byte[10];
			var rnd = new Random();
			rnd.NextBytes(byteBuffer);
			return new MemoryStream(byteBuffer);
		}

		public HttpPostedFileRandomJpg()
		{
			stream = GetRandomStream();
			ContentType = "image/jpeg";
			FileName = "test-file.jpg";
		}

		public override int ContentLength => (int)stream.Length;

		public override string ContentType { get; }

		public override string FileName { get; }

		public override Stream InputStream => stream;

		public override void SaveAs(string filename)
		{
			using (var file = File.Open(filename, FileMode.CreateNew))
				stream.CopyTo(file);
		}
	}
}
