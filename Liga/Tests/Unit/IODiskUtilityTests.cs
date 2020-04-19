using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;
using Tests.Integration;

namespace Tests.Unit
{
	[TestFixture]
	public class IODiskUtilityTests : BaseIntegrationTest
	{
		//[Test]
		//public void ActualizarDNIEnFoto()
		//{
		//	var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			

		//	var dniOriginal = "00112233";
		//	var dniActualizado = "11223344";
		//	var pathFotoOriginal = $"{CarpetaJugadoresPath}/{dniOriginal}.jpg";
		//	var pathFotoDniActualizado = $"{CarpetaJugadoresPath}/{dniActualizado}.jpg";

		//	File.Create(pathFotoOriginal);
		//	IODiskUtility.ActualizarDNIEnFoto("00112233", "11223344");
		//	Assert.AreEqual(false, File.Exists(pathFotoOriginal));
		//	Assert.AreEqual(true, File.Exists(pathFotoDniActualizado));
		//}
	}
}
