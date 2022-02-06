using System;

namespace LigaSoft.BusinessLogic
{
	public static class GeneradorDeHash
	{
		public static string GenerarAlfanumerico7Digitos(int semilla)
		{
			if (semilla <= 0 || semilla >= 10000)
				throw new ArgumentException();

			var semillaTransformada = TransformarAplicandoAlgoritmo(semilla);

			var letras = ObtenerLetras(semillaTransformada);

			var numeroFinal = semillaTransformada.ToString().PadLeft(4, '0');

			return $"{numeroFinal[0]}{letras}{numeroFinal[1]}{numeroFinal[2]}{numeroFinal[3]}";
		}


		public static string ObtenerLetras(int semilla4Digitos)
		{
			var primeraLetra = ObtenerLetra(ObtenerNumeroEnteroMenorOIgualQue25(semilla4Digitos));
			var segundaLetra = ObtenerLetra(ObtenerNumeroEnteroMenorOIgualQue25(semilla4Digitos * semilla4Digitos));
			var terceraLetra = ObtenerLetra(ObtenerNumeroEnteroMenorOIgualQue25(semilla4Digitos * semilla4Digitos * semilla4Digitos));

			return $"{primeraLetra}{segundaLetra}{terceraLetra}";
		}

		public static int TransformarAplicandoAlgoritmo(int semilla)
		{
			int ultimoDigito = semilla % 10;

			var ultimoDigito4Veces = $"{ultimoDigito}{ultimoDigito}{ultimoDigito}{ultimoDigito}";

			var ultimoDigito4VecesInt = int.Parse(ultimoDigito4Veces);

			return (semilla + ultimoDigito4VecesInt) % 10000;
		}

		public static int ObtenerNumeroEnteroMenorOIgualQue25(int numero)
		{
			var resultado = Math.Abs(numero % 100);
									
			while (resultado > 25)
			{
				resultado -= 25;
			}
			
			return resultado;
		}

		public static char ObtenerLetra(int indice)
		{
			if (indice < 0 || indice > 25)
				throw new ArgumentException();

			const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Son 26

			return letras[indice];
		}


	}
}