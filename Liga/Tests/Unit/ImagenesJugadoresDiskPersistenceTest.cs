﻿using System.IO;
using System.Linq;
using System.Web.Hosting;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades.DiskPersistence;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;
using Tests.Integration;

namespace Tests.Unit
{
	[TestFixture]
	public class ImagenesJugadoresDiskPersistenceTest
	{
		private readonly AppPathsForTest _paths;
		private readonly ImagenesJugadoresDiskPersistence _imagenesJugadoresDiskPersistence;
		private const string DNI = "12345678";
		private static JugadorBaseVM _jugadorBaseVm;
		private static string _imagePath;

		public ImagenesJugadoresDiskPersistenceTest()
		{
			_paths = new AppPathsForTest();
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(_paths);
			_imagePath = $"{_paths.ImagenesJugadoresAbsolute}/{DNI}.jpg";
			_jugadorBaseVm = new JugadorBaseVM
			{
				Foto = "data:image/png;base64,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BAgQIECAAAECBARoe4AAAQKVCviYzUobp2wCBKoXEKCrb6EFECBAgAABAgQIlBQQoEtqm4sAAQIECBAgQKB6AQG6+hZaAAECBAgQIECAQEkBAbqktrkIECBAgAABAgSqFxCgq2+hBRAgQIAAAQIECJQUEKBLapuLAAECBAgQIECgegEBuvoWWgABAgQIECBAgEBJAQG6pLa5CBAgQIAAAQIEqhcQoKtvoQUQIECAAAECBAiUFBCgS2qbiwABAgQIECBAoHoBAbr6FloAAQIECBAgQIBASQEBuqS2uQgQIECAAAECBKoXEKCrb6EFECBAgAABAgQIlBQQoEtqm4sAAQIECBAgQKB6AQG6+hZaAAECBAgQIECAQEkBAbqktrkIECBAgAABAgSqFxCgq2+hBRAgQIAAAQIECJQUEKBLapuLAAECBAgQIECgegEBuvoWWgABAgQIPBMYhuEyjiMcAgQIZBcQoLOTOiEBAgQIRBAQoCN0QQ0E2hQQoNvsq1URIECAAAECBAgcJCBAHwTrtAQIECBAgAABAm0KCNBt9tWqCBAgQIAAAQIEDhIQoA+CTT3tdeGAj9QTGU+AAAECBAgQIHCogAB9KO/6kwvQ662MJECAAAECBAicKSBAn6lvbgIECBAgQIAAgeoEBOjqWqZgAgQIECBAgACBMwUE6DP1zU2AAAECBAgQIFCdgABdXcsUTIAAAQIECBAgcKaAAH2m/sPc4/C8kMG30AbpkDIIECBAgAABAncBATrIThCggzRCGQQIECBAgACBNwICdJAtMgXo7yD9dUX6+l+QApVBgAABAgQIECDgCnSkPSBAR+qGWggQIECAAAECywKuQAfbHfNbOdwDHaxByiFAgAABAgS6FxCgg20BATpYQ5RDgAABAgQIEJgJCNC2BAECBAgQIECAAIEEAQE6ActQAgQIECBAgAABAgK0PUCAAAECBAgQIEAgQUCATsAylAABAgQIECBAgIAAbQ8QIECAAAECBAgQSBAQoBOwDCVAgAABAgQIECAgQNsDBAgQIECAAAECBBIEBOgELEMJECBAgAABAgQICND2AAECBAgQIECAAIEEAQE6ActQAgQIECBAgAABAgK0PUCAAAECBAgQIEAgQUCATsAylAABAgQIECBAgIAAbQ8QIECAQBcC4/B6mcPYBYNFEiCQQUCAzoDoFAQIECAQ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BAgQIECAAIH4AgJ0/B6pkAABAgQIECBAIJCAAB2oGUohQIAAAQIECBCILyBAx++RCgkQIECAAAECBAIJCNCBmqEUAgQIECBAgACB+AICdPweqZAAAQIECBAgQCCQgAAdqBlKIUCAAAECBAgQiC8gQMfvkQoJECBAgAABAgQCCQjQgZqhFAIECBAgQIAAgfgCAnT8HqmQAAECBAgQIEAgkIAAHagZSiFAgAABAgQIEIgvIEDH75EKCRAgQIAAAQIEAgkI0IGaoRQCBAgQIECAAIH4AgJ0/B6pkAABAgQIECBAIJCAAB2oGUohQIAAAQIECBCILyBAx++RCgkQIECAAAECBAIJCNCBmqEUAgQIECBAgACB+AICdPweqZAAAQIECBAgQCCQgAAdqBlKIUCAAAECBAgQiC9QRYAehuFTchzH+KIqJECAAAECBAgQaFogfICewvPUBSG66f1ocQQIECBAgACB8AICdPgWKZAAAQIECBAgQCCSQPgAHQlLLQQIECBAgAABAgQEaHuAAAECBAgQIECAQIKAAJ2AZSgBAgQIECBAgAABAdoeIECAAAECBAgQIJAgIEAnYBlKgAABAgQIECBAQIC2BwgQIECAAAECBAgkCAjQCViGEiBAgAABAgQIEBCg7QECBAgQIECAAAECCQICdAKWoQQIECBAgAABAgQEaHuAAAECBAgQIECAQIKAAJ2AZSgBAgQIECBAgAABAdoeIECAAAECBAgQIJAgIEAnYBlKgAABAgQIECBAQIC2BwgQIECAAAECBAgkCAjQCViGEiBAgAABAgQIEBCg7QECBAgQIECAAAECCQICdAKWoQQIECBAgAABAgQEaHuAAAECBAgQIECAQIKAAJ2AZSgBAgQIECBAgAABAdoeIECAAAECBAgQIJAgIEAnYBlKgAABAgQIECBAQIC2BwgQIECAAAECBAgkCAjQCViGlhEYhuF7onEcy0xqFgIECBAgQIDASgEBeiWUYWUEHsPzbUYBuoy7WQgQIECAAIH1AgL0eisjCRAgQIAAAQIECFwEaJuAAAECBAgQIECAQIKAAJ2AZSgBAgQIECBAgAABAdoeIECAAAECBAgQIJAgIEAnYBlKgAABAgQIECBAQIC2BwgQIECAAAECBAgkCAjQCViGEiBAgAABAgQIEBCg7QECBAgQIECAAAECCQICdAKWoQQIECBAgAABAgQEaHuAAAECBAgQIECAQIKAAJ2AZSgBAgQIECBAgAABAdoeIECAAAECBAgQIJAgIEAnYBlKgAABAgQIECBQt8AwDJdxHHctQoDexedgAgQIECBAgACBWgRu4Xn6sydEC9C1dFydBAgQIECAAAECuwQeA/TtRFtDtAC9qw0OJkCAAAECBAgQqEVgHqC3hmgBupaOq5MAAQIECBAgQCCEgAAdog2KIECAAAECBAgQqEVAgK6lU+okQIAAAQIECBAIISBAh2iDIggQIECAAIGoAtN9s1t/4SzqutS1XUCA3m7nSAIECBAgQKBxgVyf2tA4U3fLE6C7a7kFEyBAgAABAikCrkCnaPUxVoDuo89WSYAAAQIECBAgkElAgM4E6TQECBAgQIAAAQJ9CAjQffTZKgkQIECAAAECBDIJCNCZIJ2GAAECBAgQIECgDwEBuo8+WyUBAgQIECBAgEAmAQE6E6TTECBAgAABAgQI9CEgQPfRZ6skQIAAAQIECBDIJCBAZ4J0GgIECBAgQIAAgT4EBOg++myVBAgQIECAAAECmQQE6EyQTkOAAAECBAgQINCHgADdR5+tkgABAgQIECBAIJOAAJ0J0mkIECBAgAABAgT6EBCg++izVRIgQIAAAQIECGQSEKAzQToNAQIECBAgQIBAHwICdB99tkoCBAgQIECAAIFMAgJ0JkinIUCAAAECBAgQ6ENAgO6jz1ZJgAABAgQIECCQSUCAzgTpNAQIECBAgAABAn0ICNB99NkqCRAgQIAAAQIEMgkI0JkgnYYAAQIECBAgQKAPAQG6jz5bJQECBAgQIECAQCYBAToTpNMQIECAAAECBAj0ISBA99FnqyRAgAABAgQIEPgSGIbhl8U4jkk2AnQSl8EECBAgQIAAAQK1CwjQtXdQ/QQIECBAgAABAkUFpgCdeuV5KtIV6KLtMhkBAgQIECBAgEDtAgJ07R1UPwECBAgQIECAQFEBAboot8kIECBAgAABAgRqFxCga++g+gkQIECAAAECBIoKCNBFuU1GgAABAgQIECBQu4AAXXsH1U+AAAECBAgQIFBUQIAuym0yAgQIECBAgACB2gUE6No7qH4CBAgQIECAAIGiAgJ0UW6TESBAgAABAgQI1C4gQNfeQfUTIECAAAECBAgUFRCgi3KbjAABAgQIECBAoHYBAbr2DqqfAAECBAgQIECgqIAAXZTbZAQIECBAgAABArULCNC1d1D9BAgQIECAAAECRQUE6KLcJiNAgAABAgQIEKhdQICuvYPqJ0CAAAECBAgQKCogQBflNhkBAgQIECBAgEDtAgJ07R1UPwECBAgQIECAQFEBAboot8kIECBAgAABAgRqFxCga++g+gkQIECAAAECBIoKCNBFuU1GgAABAgQIECBQu4AAXXsH1U+AAAECBAgQIFBUQIAuym0yAgQIECBAgACB2gUE6No7qH4CBAgQIECAAIGiAgJ0UW6TESBAgAABAgQI1C4gQNfeQfUTIECAAAECBAgUFRCgi3KbjAABAgQIECBAoHYBAbr2DqqfAAECBAgQIECgqIAAXZTbZAQIECBAgAABArULCNC1d1D9BAgQIECAAAECRQUE6KLcJiNAgAABAgQIEKhdQICuvYPqJ0CAAAECBAgQKCogQBflNhkBAgQIECBAgEDtAgJ07R1UPwECBAgQIECAQFEBAboot8kIECBAgAABAgRqFxCga++g+gkQIECAAAECBIoKCNBFuU1GgAABAgQIECBQu4AAXXsH1U+AAAECBAgQIFBUQIAuym0yAgQIECBAgACB2gUE6No7qH4CBAgQIECAAIGiAgJ0UW6TESBAgAABAgQI1C4gQNfeQfUTIECAAAECBAgUFRCgi3KbjAABAgQIECBAoHYBAbr2DqqfAAECBHYJjMPzw4dx12kdTIBAwwICdMPNtTQCBAgQeC8gQL83MoIAgd8CArQdQYAAAQJdCSwF5jmCK9BdbQuLJZAkIEAncRlMgAABArULCNC1d1D9BM4XEKDP74EKCBAgQIAAAQIEKhIQoCtqllIJECBAgAABAgTOF1gM0NNbXO4BO79JKiBAgAABAgQIEIgjIEDH6YVKCBAgQIAAAQIEKhBwC0cFTVIiAQIECBAgQIBAHAEBOk4vVEKAAAECBAgQIFCBgABdQZOUSIAAAQIECBAgEEdAgI7TC5UQIECAAAECBAhUIDAM/67jszp9+kYF3VMiAQIECBAgQIBAcQEBuji5CQkQIECAAAECBGoWGC7/c78CPb/i7Ap0zW1VOwECBAgQIECAwFECAvRRss5LgAABAgQIECDQpIBbOJpsq0URIECAAAECBAgcJSBAHyXrvAQIECBAgAABAk0KfAdo9zw32V+LIkCAAAECBAgQyCwg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",
				DNI = DNI
			};
		}

		[SetUp]
		public void Initialize()
		{
			EliminarTodosLosArchivosEnLaCarpeta(_paths.ImagenesJugadoresAbsolute);
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
		public void GuardarFotoWebCamCuandoLaFotoNoExiste()
		{
			Assert.AreEqual(false, File.Exists(_imagePath));

			_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(_jugadorBaseVm);

			Assert.AreEqual(true, File.Exists(_imagePath));
		}

		[Test]
		public void GuardarFotoWebCamReemplazandoExistente()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			_imagenesJugadoresDiskPersistence.GuardarFotoWebCam(_jugadorBaseVm);

			Assert.AreEqual(true, File.Exists(_imagePath));
		}

		[Test]
		public void GetFotoEnBase64()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			Assert.IsNotEmpty(_imagenesJugadoresDiskPersistence.GetFotoEnBase64(DNI));
		}

		[Test]
		public void Eliminar()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			_imagenesJugadoresDiskPersistence.Eliminar(DNI);

			Assert.AreEqual(false, File.Exists(_imagePath));
		}

		[Test]
		public void CambiarDNI()
		{
			GuardarFotoWebCamCuandoLaFotoNoExiste();

			const string nuevoDni = "22334400";
			_imagenesJugadoresDiskPersistence.CambiarDNI(DNI, nuevoDni);

			Assert.AreEqual(false, File.Exists(_imagePath));
			Assert.AreEqual(true, File.Exists($"{_paths.ImagenesJugadoresAbsolute}/{nuevoDni}.jpg"));
		}
	}
}
