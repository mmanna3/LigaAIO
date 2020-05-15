using System;
using System.IO;
using LigaSoft.Utilidades;
using NUnit.Framework;
using Tests.Integration.Utilidades;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenUtilityTest
	{		
		[Test]
		public void ConvertirABitMapYATamanio240X240()
		{
			var bitMap = ImagenUtility.ConvertirABitMapYATamanio240X240(Constantes.puntoRojoBase64ConUriDataJpg);
			Assert.AreEqual(240, bitMap.Width);
			Assert.AreEqual(240, bitMap.Height);
		}

		[Test]
		public void ImagenCuadradaRotarAHorizontalYComprimir()
		{
			var bytes = Convert.FromBase64String(Constantes.rectanguloVerticalBase64);
			var stream = new MemoryStream(bytes);

			var image = ImagenUtility.RotarAHorizontalYComprimir(stream);

			Assert.IsTrue(image.Width > image.Height);
		}

	}
}
