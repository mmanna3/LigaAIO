using System;
using System.Linq;
using LigaSoft.Builders;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;

namespace Tests.Integration
{
	[TestFixture]
	public class ZonaHelperTests : BaseIntegrationTest
	{
		private readonly ZonaHelper _zonaHelper;

		public ZonaHelperTests()
		{
			_zonaHelper = new ZonaHelper(Context);
		}

		//[Test]
		//public void AlIntentarObtenerZonaClausuraDeZonaClausuraDevuelveNull()
		//{
		//	var zonaClausura = Context.Zonas.First(x => x.Tipo == ZonaTipo.Clausura);
		//	Assert.IsNull(zonaClausura);
		//}

		//[Test]
		//public void AlIntentarObtenerZonaClausuraDeZonaRelampagoDevuelveNull()
		//{
		//	var zonaRelampago = Context.Zonas.First(x => x.Tipo == ZonaTipo.Relampago);
		//	Assert.IsNull(zonaRelampago);
		//}

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
