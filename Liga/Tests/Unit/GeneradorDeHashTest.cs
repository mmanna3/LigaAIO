using System;
using System.IO;
using LigaSoft.Utilidades;
using LigaSoft.BusinessLogic;
using NUnit.Framework;
using Tests.Integration.Utilidades;

namespace Tests.Unit
{
	[TestFixture]
	public class GeneradorDeHashTest
	{
		[Test]
		public void Genera_Correctamente()
		{
			var hash = GeneradorDeHash.GenerarAlfanumerico7Digitos(1);
			Assert.AreEqual("1MTD112", hash);

			var hash2 = GeneradorDeHash.GenerarAlfanumerico7Digitos(2);
			Assert.AreEqual("2YBO224", hash2);

			var hash22 = GeneradorDeHash.GenerarAlfanumerico7Digitos(22);
			Assert.AreEqual("2TLE244", hash22);

			var hash3 = GeneradorDeHash.GenerarAlfanumerico7Digitos(23);
			Assert.AreEqual("3GLX356", hash3);

			var hash4 = GeneradorDeHash.GenerarAlfanumerico7Digitos(10);
			Assert.AreEqual("0KAA010", hash4);

			var hash5 = GeneradorDeHash.GenerarAlfanumerico7Digitos(100);
			Assert.AreEqual("0AAA100", hash5);

			var hash6 = GeneradorDeHash.GenerarAlfanumerico7Digitos(2456);
			Assert.AreEqual("9WJT122", hash6);
		}

		[Test]
		public void Semilla_TieneQueSer_MayorQue0_Y_MenorQue10000()
		{
			Assert.Throws<ArgumentException>(() => GeneradorDeHash.GenerarAlfanumerico7Digitos(-1));
			Assert.Throws<ArgumentException>(() => GeneradorDeHash.GenerarAlfanumerico7Digitos(0));
			Assert.Throws<ArgumentException>(() => GeneradorDeHash.GenerarAlfanumerico7Digitos(10000));
		}

		[Test]
		public void TransformarSemilla_En_NumeroDe4Digitos()
		{
			var hash = GeneradorDeHash.TransformarAplicandoAlgoritmo(2);
			Assert.AreEqual(2224, hash);

			var hash2 = GeneradorDeHash.TransformarAplicandoAlgoritmo(1);
			Assert.AreEqual(1112, hash2);

			var hash3 = GeneradorDeHash.TransformarAplicandoAlgoritmo(1000);
			Assert.AreEqual(1000, hash3);

			var hash4 = GeneradorDeHash.TransformarAplicandoAlgoritmo(7779);
			Assert.AreEqual(7778, hash4);
		}

		[Test]
		public void Obtener_NumeroEntero_MenorOIgualQue25()
		{
			var resultado1 = GeneradorDeHash.ObtenerNumeroEnteroMenorOIgualQue25(14);
			Assert.AreEqual(14, resultado1);

			var resultado2 = GeneradorDeHash.ObtenerNumeroEnteroMenorOIgualQue25(100);
			Assert.AreEqual(00, resultado2);
			
			var resultado3 = GeneradorDeHash.ObtenerNumeroEnteroMenorOIgualQue25(26);
			Assert.AreEqual(1, resultado3);

			var resultado4 = GeneradorDeHash.ObtenerNumeroEnteroMenorOIgualQue25(99);
			Assert.AreEqual(24, resultado4);
		}

		[Test]
		public void Obtener_Letras()
		{
			var resultado1 = GeneradorDeHash.ObtenerLetras(1112);
			Assert.AreEqual("MTD", resultado1);

			var resultado2 = GeneradorDeHash.ObtenerLetras(2224);
			Assert.AreEqual("YBO", resultado2);

			var resultado3 = GeneradorDeHash.ObtenerLetras(3336);
			Assert.AreEqual("LVI", resultado3);

			var resultado4 = GeneradorDeHash.ObtenerLetras(1245);
			Assert.AreEqual("UZZ", resultado4);

			var resultado5 = GeneradorDeHash.ObtenerLetras(9999);
			Assert.AreEqual("YBT", resultado5);
		}

	}
}
