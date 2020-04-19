using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Hosting;
using Ionic.Zip;
using LigaSoft.Models.Dominio;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Utilidades
{
	public class ZipUtility
	{
		private static ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;

		public ZipUtility()
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}


		public static IEnumerable<Jugador> Importar(Stream inputStream, out List<string> mensajeResultado)
		{
			var result = new List<Jugador>();
			var fileSystem = Descomprimir(inputStream);

			var jugFile = fileSystem.Single(x => x.Key.Contains(".jug"));
			var jugadoresStr = System.Text.Encoding.UTF8.GetString(jugFile.Value);
			var jugadoresArray = jugadoresStr.Replace("\n", "").Split('\r');
			jugadoresArray = jugadoresArray.Take(jugadoresArray.Length - 1).ToArray(); //Siempre hay uno de más en blanco.

			mensajeResultado = new List<string>
			{ $"El archivo contiene {jugadoresArray.Length} jugadores." };

			foreach (var linea in jugadoresArray)
			{
				try
				{
					var datos = linea.Split(';');
					var jug = new Jugador
					{
						DNI = datos[0],
						Nombre = datos[1],
						Apellido = datos[2],
						FechaNacimiento = ConvertToDateTime(datos[3]),						
					};
					GuardarFotoEnDisco(datos[0], fileSystem);

					result.Add(jug);
				}
				catch(Exception e)
				{
					mensajeResultado.Add($"En la línea: {linea} se produjo el siguiente error: {e.Message}");
				}
			}
			
			return result;
		}

		private static DateTime ConvertToDateTime(string value)
		{
			string[] formatos = {"dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
				"dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy"};

			DateTime.TryParseExact(value, formatos, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var fecha);

			return fecha;
		}

		private static void GuardarFotoEnDisco(string dni, Dictionary<string, byte[]> fileSystem)
		{
			var fotoByteArray = fileSystem.Single(x => x.Key.Contains(dni)).Value;
			_imagenesJugadoresDiskPersistence.GuardarImagenJugadorImportado(dni, fotoByteArray);
		}

		private static Dictionary<string, byte[]> Descomprimir(Stream targFileStream)
		{
			var files = new Dictionary<string, byte[]>();

			using (var z = ZipFile.Read(targFileStream))
				foreach (var zEntry in z)
				{
					var tempS = new MemoryStream();
					zEntry.Extract(tempS);

					files.Add(zEntry.FileName, tempS.ToArray());
				}

			return files;
		}
	}
}