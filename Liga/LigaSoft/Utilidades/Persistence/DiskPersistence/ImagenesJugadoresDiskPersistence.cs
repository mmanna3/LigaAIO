﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.Utilidades.Persistence.DiskPersistence
{
	public class ImagenesJugadoresDiskPersistence : IImagenesJugadoresPersistence
	{
		private static AppPaths Paths;

		public ImagenesJugadoresDiskPersistence(AppPaths appPaths)
		{
			Paths = appPaths;
		}

		public void GuardarFotoWebCam(JugadorBaseVM vm)
		{
			var foto = ImagenUtility.ConvertirABitMapYATamanio240X240YEspejar(vm.Foto);
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);		
			foto.Save(imagePath);
		}

		public void GuardarFotoDeJugadorDesdeArchivo(EditFotoJugadorDesdeArchivoVM vm)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{vm.DNI}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);
			vm.Foto.SaveAs(imagePath);
		}

		////No testeado
		//public void GuardarFotosTemporalesDeJugadorFichadoPorDelegado(JugadorFichadoPorDelegadoVM vm)
		//{
		//	GuardarFotoCarnetTemporal(vm);
		//	GuardarFotoDNIFrenteTemporal(vm);
		//}

		public string PathFotoTemporalCarnet(string dni)
		{
			return $"{Paths.ImagenesTemporalesJugadorCarnetRelative}/{dni}.jpg";
		}

		public string PathFotoTemporalDNIFrente(string dni)
		{
			return $"{Paths.ImagenesTemporalesJugadorDNIFrenteRelative}/{dni}.jpg";
		}

		public string PathFotoTemporalDNIDorso(string dni)
		{
			return $"{Paths.ImagenesTemporalesJugadorDNIDorsoRelative}/{dni}.jpg";
		}

		//No testeado
		public void FicharJugadorTemporal(string dni)
		{
			var pathTemporal = $"{Paths.ImagenesTemporalesJugadorCarnetAbsolute}/{dni}.jpg";
			var pathJugadores = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			Directory.CreateDirectory(Paths.ImagenesJugadoresAbsolute);
			
			// Si por algo quedó una foto de este jugador en el disco, aunque el jugador no figure fichado
			// (Quizás porque se borró el jugador pero no la foto)
			// De más está decir que esto no debería pasar, pero pasó, entonces puse este bonito IF
			if (File.Exists(pathJugadores))
				File.Delete(pathJugadores);

			if (File.Exists(pathTemporal))
				File.Move(pathTemporal, pathJugadores);

			var pathDNIFrente = $"{Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{dni}.jpg";

			if (File.Exists(pathDNIFrente))
				File.Delete(pathDNIFrente);

			var pathDNIDorso = $"{Paths.ImagenesTemporalesJugadorDNIDorsoAbsolute}/{dni}.jpg";

			if (File.Exists(pathDNIDorso))
				File.Delete(pathDNIDorso);
		}

		public void GuardarFotosTemporalesDeJugadorAutofichado(JugadorAutofichadoVM vm)
		{
			GuardarFotoCarnetTemporal(new JugadorAutofichadoVM {DNI = vm.DNI, FotoCarnet = vm.FotoCarnet });
			GuardarFotoDNIFrenteTemporal(vm);
			GuardarFotoDNIDorsoTemporal(vm);
		}

		public void GuardarFotosTemporalesDeJugadorAutofichadoSiendoEditado(JugadorAutofichadoVM vm)
		{
			if (vm.FotoCarnet != null)
				GuardarFotoCarnetTemporal(new JugadorAutofichadoVM { DNI = vm.DNI, FotoCarnet = vm.FotoCarnet });
			
			if (vm.FotoDNIFrente != null)
				GuardarFotoDNIFrenteTemporal(vm);

			if (vm.FotoDNIDorso != null)
				GuardarFotoDNIDorsoTemporal(vm);
		}

		private static void GuardarFotoCarnetTemporal(JugadorAutofichadoVM vm)
		{
			if (vm.FotoCarnet != null)
			{
				var imagePath = $"{Paths.ImagenesTemporalesJugadorCarnetAbsolute}/{vm.DNI}.jpg";

				if (File.Exists(imagePath))
					File.Delete(imagePath);

				Directory.CreateDirectory(Paths.ImagenesTemporalesJugadorCarnetAbsolute);

				var result = ImagenUtility.ConvertirABitMapYATamanio240X240(vm.FotoCarnet);
				result.Save(imagePath);
			}
		}

		private static void GuardarFotoDNIFrenteTemporal(JugadorAutofichadoVM vm)
		{
			if (vm.FotoDNIFrente != null)
			{
				var imagePath = $"{Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{vm.DNI}.jpg";

				if (File.Exists(imagePath))
					File.Delete(imagePath);

				Directory.CreateDirectory(Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute);
				var result = ImagenUtility.Comprimir(vm.FotoDNIFrente);
				result.Save(imagePath);
			}
		}

		private static void GuardarFotoDNIDorsoTemporal(JugadorAutofichadoVM vm)
		{
			if (vm.FotoDNIDorso != null)
			{
				var imagePath = $"{Paths.ImagenesTemporalesJugadorDNIDorsoAbsolute}/{vm.DNI}.jpg";

				if (File.Exists(imagePath))
					File.Delete(imagePath);

				Directory.CreateDirectory(Paths.ImagenesTemporalesJugadorDNIDorsoAbsolute);
				var result = ImagenUtility.Comprimir(vm.FotoDNIDorso);
				result.Save(imagePath);
			}
		}

		//private static void GuardarFotoDNIFrenteTemporal(JugadorFichadoPorDelegadoVM vm)
		//{
		//	if (vm.FotoDNIFrente != null)
		//	{
		//		var imagePath = $"{Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{vm.DNI}.jpg";

		//		if (File.Exists(imagePath))
		//			File.Delete(imagePath);

		//		Directory.CreateDirectory(Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute);
		//		var result = ImagenUtility.RotarAHorizontalYComprimir(vm.FotoDNIFrente.InputStream);
		//		result.Save(imagePath);
		//	}
		//}

		public string GetFotoEnBase64(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";
			using (var stream = new FileStream(imagePath, FileMode.Open))
			using (var image = Image.FromStream(stream))
				return ImagenUtility.ImageToBase64(image);
		}

		public string Path(string dni)
		{
			return $"{Paths.ImagenesJugadoresRelative}/{dni}.jpg";
		}

		//No testeado
		public void GuardarImagenJugadorImportado(string dni, byte[] fotoByteArray)
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

		public void EliminarLista(IList<string> dnis)
		{
			foreach (var dni in dnis)
				Eliminar(dni);
		}

		public void Eliminar(string dni)
		{
			var imagePath = $"{Paths.ImagenesJugadoresAbsolute}/{dni}.jpg";

			if (File.Exists(imagePath))
				File.Delete(imagePath);
		}

		public void CambiarDNI(string dniAnterior, string dniNuevo)
		{
			var pathAnterior = $"{Paths.ImagenesJugadoresAbsolute}/{dniAnterior}.jpg";
			var pathNuevo = $"{Paths.ImagenesJugadoresAbsolute}/{dniNuevo}.jpg";

			if (File.Exists(pathAnterior))
				File.Move(pathAnterior, pathNuevo);
		}

		public void RenombrarFotosTemporalesPorCambioDeDNI(string DNIAnterior, string DNINuevo)
		{
			RenombrarFotos($"{Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{DNIAnterior}.jpg", $"{Paths.ImagenesTemporalesJugadorDNIFrenteAbsolute}/{DNINuevo}.jpg");
			RenombrarFotos($"{Paths.ImagenesTemporalesJugadorDNIDorsoAbsolute}/{DNIAnterior}.jpg", $"{Paths.ImagenesTemporalesJugadorDNIDorsoAbsolute}/{DNINuevo}.jpg");
			RenombrarFotos($"{Paths.ImagenesTemporalesJugadorCarnetAbsolute}/{DNIAnterior}.jpg", $"{Paths.ImagenesTemporalesJugadorCarnetAbsolute}/{DNINuevo}.jpg");
		}

		private static void RenombrarFotos(string pathAnterior, string pathNuevo)
		{
			if (File.Exists(pathNuevo))
				File.Delete(pathNuevo);

			if (File.Exists(pathAnterior))
				File.Move(pathAnterior, pathNuevo);
		}
	}
}