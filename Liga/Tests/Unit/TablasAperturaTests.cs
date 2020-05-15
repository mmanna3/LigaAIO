using System.Linq;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.ViewModels;
using NUnit.Framework;
using Tests.Unit.Utilidades;

namespace Tests.Unit
{
	[TestFixture]
	public class TablasAperturaTests : BaseUnitTest
	{
		private readonly TablaCategoriaVM _tablaCategoriaSegunda;
		private readonly TablaCategoriaVM _tablaCategoriaPrimera;
		private readonly TablaCategoriaVM _tablaGeneral;

		public TablasAperturaTests()
	    {
		    var tablaBuilder = new TablaWebPublicaBuilder(Context);
			var zonaApertura = Context.Zonas.Single(x => x.Tipo == LigaSoft.Models.Enums.ZonaTipo.Apertura);
		    var tablasVM = tablaBuilder.Tablas(zonaApertura);

			_tablaCategoriaSegunda = tablasVM.TablasPorCategoria.Single(x => x.Categoria == "Segunda");
		    _tablaCategoriaPrimera = tablasVM.TablasPorCategoria.Single(x => x.Categoria == "Primera");
		    _tablaGeneral = tablasVM.TablaGeneral;
		}

		[Test]
		public void EnCategoriaSegundaRacingJugoSoloUnPartidoPorqueUnoSePostergoYElOtroSeSuspendio()
		{
			var renglonRacing = _tablaCategoriaSegunda.Renglones.Single(x => x.Equipo == "Racing");
			Assert.AreEqual(1, renglonRacing.Pj);
		}

		[Test]
		public void EnCategoriaPrimeraBocaGanoTodosLosPartidosQueJugoYDeberiaEstarPrimeroCon9Puntos()
		{
			var renglonBoca = _tablaCategoriaPrimera.Renglones.Single(x => x.Equipo == "Boca");
			Assert.AreEqual(9, renglonBoca.Pts);
		}

		[Test]
		public void EnCategoriaSegundaBocaGanoUnoPerdioUnoYTuvoUnoSuspendidoDeberiaTener4Pts()
		{
			var renglonBoca = _tablaCategoriaSegunda.Renglones.Single(x => x.Equipo == "Boca");
			Assert.AreEqual(1, renglonBoca.Pg);
			Assert.AreEqual(0, renglonBoca.Pe);
			Assert.AreEqual(1, renglonBoca.Pp);
			Assert.AreEqual(4, renglonBoca.Pts);
		}

		[Test]
		public void EnGeneralBocaDeberiaTener13Pts()
		{
			var renglonBoca = _tablaGeneral.Renglones.Single(x => x.Equipo == "Boca");
			Assert.AreEqual(13, renglonBoca.Pts);
		}
	}
}
