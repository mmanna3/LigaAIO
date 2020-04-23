using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace LigaSoft.Utilidades
{
	public static class ImagenUtility
	{
		public static Bitmap ConvertirABitMapYATamanio240X240(string fotoBase64)
		{
			fotoBase64 = QuitarMimeType(fotoBase64);
			var fotoCuadrada = HacerFotoCuadrada240X240(fotoBase64);
			return fotoCuadrada;
		}

		public static Bitmap ConvertirABitMapYATamanio240X240YEspejar(string fotoBase64)
		{
			var result = ConvertirABitMapYATamanio240X240(fotoBase64);
			result.EspejarImagen();
			return result;
		}

		public static Image ConvertirAImageYQuitarMimeType(string imagenBase64conMimeType)
		{
			imagenBase64conMimeType = QuitarMimeType(imagenBase64conMimeType);
			return Base64ToImage(imagenBase64conMimeType);
		}

		public static Image RotarAHorizontalYComprimir(Stream stream)
		{
			var image = Image.FromStream(stream);

			if (EsVertical(image))
				image.RotateFlip(RotateFlipType.Rotate90FlipXY);

			image = ResizeImageFixedWidth(image, 500);

			return image;
		}

		public static string StreamToBase64(Stream stream)
		{
			using (var image = Image.FromStream(stream))
				return ImageToBase64(image);
		}

		public static string ImageToBase64(Image img)
		{
			using (var image = img)
			using (var m = new MemoryStream())
			{
				image.Save(m, ImageFormat.Png);

				return ByteArrayToBase64(m.ToArray());
			}
		}

		private static bool EsVertical(Image image)
		{
			return image.Width < image.Height;
		}

		private static string ByteArrayToBase64(byte[] foto)
		{
			return Convert.ToBase64String(foto);
		}

		private static void EspejarImagen(this Image imagen)
		{
			imagen.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}

		private static Bitmap HacerFotoCuadrada240X240(string fotoBase64)
		{
			var fotoBmp = Base64ToImage(fotoBase64);
			return HacerFotoCuadrada(fotoBmp, 240);			
		}

		private static Image Base64ToImage(string base64String)
		{
			var imageBytes = Convert.FromBase64String(base64String);

			using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
			{
				var image = Image.FromStream(ms, true);
				return image;
			}
		}

		private static Bitmap HacerFotoCuadrada(Image bmp, int size)
		{
			var res = new Bitmap(size, size);
			var g = Graphics.FromImage(res);

			g.FillRectangle(new SolidBrush(Color.White), 0, 0, size, size);

			int t = 0, l = 0;
			if (bmp.Height > bmp.Width)
				t = (bmp.Height - bmp.Width) / 2;
			else
				l = (bmp.Width - bmp.Height) / 2;

			g.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(l, t, bmp.Width - l * 2, bmp.Height - t * 2), GraphicsUnit.Pixel);

			return res;
		}

		private static string QuitarMimeType(string base64)
		{
			var i = base64.IndexOf(',');
			if (i > 0)
				base64 = base64.Substring(i + 1).Trim();

			return base64;
		}

		public static Image ResizeImageFixedWidth(Image imgToResize, int width)
		{
			var sourceWidth = imgToResize.Width;
			var sourceHeight = imgToResize.Height;

			var nPercent = ((float)width / (float)sourceWidth);

			var destWidth = (int)(sourceWidth * nPercent);
			var destHeight = (int)(sourceHeight * nPercent);

			var b = new Bitmap(destWidth, destHeight);
			var g = Graphics.FromImage((Image)b);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
			g.Dispose();

			return (Image)b;
		}
	}
}