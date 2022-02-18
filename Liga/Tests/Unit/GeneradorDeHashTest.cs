using System;
using LigaSoft.BusinessLogic;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class GeneradorDeHashTest
	{
		[Test]
		public void ObtieneSemilla_ApartirDeHash_Correctamente()
		{
			var semilla = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("MTD0001");
			Assert.AreEqual(1, semilla);

			var semilla2 = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("WJT2456");
			Assert.AreEqual(2456, semilla2);

			var semilla3 = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("WJT8011");
			Assert.AreEqual(8011, semilla3);
		}

		[Test]
		public void ObtieneSemilla_ApartirDeHash_Falla()
		{
			Assert.Throws<Exception>(() => GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("0MTD001A"), "El código debe ser de 7 dígitos");

			Assert.Throws<Exception>(() => GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("MTD00M1"), "El código no tiene el formato correcto");

			Assert.Throws<Exception>(() => GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos("XXX0001"), "El código es incorrecto");
		}

		[Test]
		public void Genera_Correctamente()
		{
			var hash = GeneradorDeHash.GenerarAlfanumerico7Digitos(1);
			Assert.AreEqual("MTD0001", hash);

			var hash2 = GeneradorDeHash.GenerarAlfanumerico7Digitos(2);
			Assert.AreEqual("YBO0002", hash2);

			var hash22 = GeneradorDeHash.GenerarAlfanumerico7Digitos(22);
			Assert.AreEqual("TLE0022", hash22);

			var hash3 = GeneradorDeHash.GenerarAlfanumerico7Digitos(23);
			Assert.AreEqual("GLX0023", hash3);

			var hash4 = GeneradorDeHash.GenerarAlfanumerico7Digitos(10);
			Assert.AreEqual("KAA0010", hash4);

			var hash5 = GeneradorDeHash.GenerarAlfanumerico7Digitos(100);
			Assert.AreEqual("AAA0100", hash5);

			var hash6 = GeneradorDeHash.GenerarAlfanumerico7Digitos(2456);
			Assert.AreEqual("WJT2456", hash6);

			var hash8011 = GeneradorDeHash.GenerarAlfanumerico7Digitos(8011);
			Assert.AreEqual("WJT8011", hash8011);
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
