using System.Linq;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using NUnit.Framework;
using Tests.Unit.Utilidades;

namespace Tests.Unit
{
	[TestFixture]
	public class ZonaHelperTests : BaseUnitTest
	{
		private readonly ZonaHelper _zonaHelper;

		public ZonaHelperTests()
		{
			_zonaHelper = new ZonaHelper(Context);
		}

		[Test]
		public void AlIntentarObtenerZonaClausuraDeZonaAperturaDevuelveZonaClausuraDelTorneoDeMismoNombre()
		{
			var zonaApertura = Context.Zonas.First(x => x.Tipo == ZonaTipo.Apertura);
			var zonaClausura = _zonaHelper.ZonaClausura(zonaApertura);

			Assert.AreEqual(ZonaTipo.Clausura, zonaClausura.Tipo);
			Assert.AreEqual(zonaApertura.TorneoId, zonaClausura.TorneoId);
			Assert.AreEqual(zonaApertura.Nombre, zonaClausura.Nombre);
		}

		[Test]
		public void AlIntentarObtenerZonaClausuraDeZonaAperturaSinZonaClausuraDevuelveNull()
		{
			var zonaApertura = new Zona
			{
				Tipo = ZonaTipo.Apertura,
				Nombre = "Apertura2"
			};

			var zonaClausura = _zonaHelper.ZonaClausura(zonaApertura);

			Assert.IsNull(zonaClausura);
		}
	}
}
