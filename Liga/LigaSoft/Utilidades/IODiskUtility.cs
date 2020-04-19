using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Ionic.Zip;
using LigaSoft.Models;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades
{
	public class IODiskUtility
	{
		public const string FormatoFechaBackup = "yyyy-MM-dd--HH-mm-ss";

		private static readonly ApplicationDbContext Context = new ApplicationDbContext();
		private static readonly AppPathsWebApp Paths = new AppPathsWebApp();

		public static void GuardarFotoWebCamDeJugadorEnDisco(JugadorBaseVM vm)
		{
			var foto = ImagenUtility.ProcesarImagenDeCamaraWebParaGuardarEnDisco(vm.Foto);
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))				
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);		
			foto.Save(imagePath);
		}

		public static void GuardarFotoDeJugadorDesdeArchivoEnDisco(EditFotoJugadorDesdeArchivoVM vm)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);
			vm.Foto.SaveAs(imagePath);
		}

		public static void GuardarFotoDeEscudoEnDisco(CargarEscudoVM vm)
		{
			var imagePath = $"{Paths.ImagenesEscudosAbsolute}/{vm.ClubId}.jpg";			

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesEscudosAbsolute);
			vm.Escudo.SaveAs(imagePath);
		}

		public static string GetFotoJugadorEnBase64(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";
			using (var stream = new FileStream(imagePath, FileMode.Open))
				using (var image = Image.FromStream(stream))
					return ImagenUtility.ImageToBase64(image);
		}

		public static void EliminarEscudo(int id)
		{
			var imagePath = $"{Paths.ImagenesEscudosAbsolute}/{id}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);
		}

		public static string EscudoPath(int clubId)
		{
			var escudoPathRelativo = $"{Paths.ImagenesEscudosRelative}/{clubId}.jpg";
			var escudoPathAbsoluto = $"{Paths.ImagenesEscudosAbsolute}/{clubId}.jpg";
			if (File.Exists(escudoPathAbsoluto))
				return escudoPathRelativo;

			return ImagenUtility.ProcesarImagenDeBDParaMostrarEnWeb(Context.ParametrizacionesGlobales.First().EscudoPorDefectoEnBase64);
		}

		public static string FotoJugadorPath(string dni)
		{
			return $"{Paths.ImagenesJugadoresRelative}/{dni}.jpg";
		}

		public static void GuardarFotoDeJugadorImportadoEnDisco(string dni, byte[] fotoByteArray)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);

			using (var image = Image.FromStream(new MemoryStream(fotoByteArray)))
			{
				image.Save(imagePath);
			}
		}

		public static void EliminarFotoDeJugador(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);
		}

		public static void GuardarFotoDePublicidadEnDisco(PublicidadVM vm)
		{
			var imagePath = $"{Paths.ImagenesPublicidadesAbsolute}/{vm.Id}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesPublicidadesAbsolute);
			vm.ImagenNueva.SaveAs(imagePath);
		}

		public static string PublicidadImagenPath(int id)
		{
			return $"{Paths.ImagenesPublicidadesRelative}/{id}.jpg";
		}

		// ReSharper disable AssignNullToNotNullAttribute
		public static string ComprimirImagenesYPonerZipEnAppData()
		{
			var imagenesPath = Paths.ImagenesAbsolute;
			var backupPath = Paths.BackupAbsoluteOf($"Imagenes-{DateTime.Now.ToString(FormatoFechaBackup)}.zip");

			try
			{
				using (var zip = new ZipFile())
				{
					zip.AddDirectory(imagenesPath);
					Log.Info($"Se comprimió correctamente la carpeta '{imagenesPath}'.");
					zip.Save(backupPath);
					Log.Info($"Se guardó la carpeta comprimida en '{backupPath}'.");
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error comprimiendo imágenes");
			}

			return backupPath;
		}

		private static void EliminarArchivos(string folderPath, string fileSearchPattern)
		{
			Log.Info($"Se van a eliminar todos los archivos de '{folderPath}' con extensión: '{fileSearchPattern}'.");
			var filePaths = Directory.GetFiles(folderPath, fileSearchPattern);
			Log.Info($"Cantidad de archivos con extensión '{fileSearchPattern}' encontrados en la carpeta: '{filePaths.Length}'.");
			foreach (var filePath in filePaths)
			{
				Log.Info($"Se intenta eliminar el archivo: '{filePath}'.");
				if (File.Exists(filePath))
				{
					try
					{
						File.Delete(filePath);
						Log.Info("Se eliminó correctamente.");
					}
					catch (Exception ex)
					{
						YKNExHandler.LoguearYLanzarExcepcion(ex, "Error al intentar eliminar el archivo.");
					}
				}
				else
				{
					Log.Info("El archivo no existía.");
				}
					
			}
		}

		public static string ComprimirUltimoBackupBdYPonerZipEnAppData()
		{
			var bdBackupPathSinComprimir = PathDelUltimoBackupBD();
			if (bdBackupPathSinComprimir == null)
				YKNExHandler.LoguearYLanzarExcepcion(new Exception(), "No hay backups de base de datos");
			else
				Log.Info($"El backup sin comprimir de la base está en: {bdBackupPathSinComprimir}.");

			var backupBdComprimidoPath = Paths.BackupAbsoluteOf($"BaseDeDatos-{DateTime.Now.ToString(FormatoFechaBackup)}.zip");

			try
			{
				using (var zip = new ZipFile())
				{
					zip.AddFile(bdBackupPathSinComprimir);
					Log.Info($"Se comprimió correctamente el archivo '{bdBackupPathSinComprimir}'.");
					zip.Save(backupBdComprimidoPath);
					Log.Info($"Se guardó el archivo comprimido en '{backupBdComprimidoPath}'.");
				}
			}
			catch (Exception ex)
			{
				YKNExHandler.LoguearYLanzarExcepcion(ex, "Error comprimiendo base de datos");
			}

			return backupBdComprimidoPath;
		}

		private static string PathDelUltimoBackupBD()
		{
			var backupPaths = Directory.GetFiles(Paths.BackupAbsolute(), "mmmannna3_edefi_prod_*.bak");

			DateTime? fechaDelBackupMasNuevo = null;
			string pathDelBackupMasNuevo = null;
			foreach (var backupPath in backupPaths)
			{
				var fechaDeEsteBackup = File.GetCreationTime(backupPath);

				if (fechaDelBackupMasNuevo == null || fechaDeEsteBackup > fechaDelBackupMasNuevo)
				{
					fechaDelBackupMasNuevo = fechaDeEsteBackup;
					pathDelBackupMasNuevo = backupPath;
				}					
			}

			return pathDelBackupMasNuevo;
		}

		public static void ActualizarDNIEnFoto(string dniAnterior, string dniNuevo)
		{
			var pathAnterior = $"{Paths.ImagenesJugadoresAbsolute}/{dniAnterior}.jpg";
			var pathNuevo = $"{Paths.ImagenesJugadoresAbsolute}/{dniNuevo}.jpg";

			if (File.Exists(pathAnterior))
				File.Move(pathAnterior, pathNuevo);
		}

		public static void EliminarTodosLosArchivosDeLaCarpetaDondeEstanLosBackups()
		{
			EliminarArchivos(Paths.BackupAbsolute(), "*.*");
		}
	}
}