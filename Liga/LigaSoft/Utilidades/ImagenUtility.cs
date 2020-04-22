using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LigaSoft.Utilidades
{
	public static class ImagenUtility
	{
		public static Bitmap ProcesarFotoBase64ParaGuardarEnDisco(string fotoBase64)
		{
			fotoBase64 = QuitarMimeType(fotoBase64);
			var fotoCuadrada = HacerFotoCuadrada240X240(fotoBase64);
			return fotoCuadrada;
		}

		public static Bitmap ProcesarImagenDeCamaraWebParaGuardarEnDisco(string fotoBase64)
		{
			var foto = ProcesarFotoBase64ParaGuardarEnDisco(fotoBase64);
			foto.EspejarImagen();   //Porque la imagen de la webcam viene invertida
			return foto;
		}

		public static string StreamToBase64(Stream stream)
		{
			using (var image = Image.FromStream(stream))
				return ImageToBase64(image);
		}

		public static string ByteArrayToBase64(byte[] foto)
		{
			return Convert.ToBase64String(foto);
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
	}
}