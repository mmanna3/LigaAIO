﻿using System.IO;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using NUnit.Framework;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesEscudosDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesEscudosDiskPersistence _imagenesEscudosDiskPersistence;
		private const int CLUBID = 1;
		private static string _escudoPath;
		private const string ESCUDOBASE64 = "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";

		public ImagenesEscudosDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesEscudosDiskPersistence = new ImagenesEscudosDiskPersistence(_paths);
			_escudoPath = $"{_paths.ImagenesEscudosAbsolute}/{CLUBID}.jpg";
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.ImagenesEscudosAbsolute);
		}

		private static void EliminarTodosLosArchivosEnLaCarpeta(string path)
		{
			if (Directory.Exists(path))
			{
				var filePaths = Directory.GetFiles(path, "*");
				foreach (var filePath in filePaths)
					File.Delete(filePath);
			}
		}

		[Test]
		public void Guardar()
		{
			GuardarEscudoRandomEnDisco(CLUBID);
			Assert.AreEqual(true, File.Exists(_escudoPath));
		}

		[Test]
		public void Eliminar()
		{
			GuardarEscudoRandomEnDisco(CLUBID);

			_imagenesEscudosDiskPersistence.Eliminar(CLUBID);

			Assert.AreEqual(false, File.Exists(_escudoPath));
		}

		[Test]
		public void GuardarEscudoDefault()
		{
			Assert.AreEqual(false, File.Exists(_paths.EscudoDefaultFileAbsolute));
			_imagenesEscudosDiskPersistence.GuardarEscudoDefault(ESCUDOBASE64);
			Assert.AreEqual(true, File.Exists(_paths.EscudoDefaultFileAbsolute));
		}

		[Test]
		public void SiEscudoNoExisteDevuelvePathDeEscudoDefault()
		{
			Assert.AreEqual(false, File.Exists(_escudoPath));
			Assert.AreEqual(_paths.EscudoDefaultRelative, _imagenesEscudosDiskPersistence.PathRelativo(CLUBID));
		}

		[Test]
		public void SiEscudoExisteDevuelveSuPath()
		{
			GuardarEscudoRandomEnDisco(CLUBID);
			Assert.AreEqual($"{_paths.ImagenesEscudosRelative}/{CLUBID}.jpg", _imagenesEscudosDiskPersistence.PathRelativo(CLUBID));
		}

		private void GuardarEscudoRandomEnDisco(int clubId)
		{
			var testImageFile = new HttpPostedFileRandomJpg();

			var vm = new CargarEscudoVM
			{
				ClubId = clubId,
				Escudo = testImageFile
			};

			_imagenesEscudosDiskPersistence.Guardar(vm);
		}
	}
}
