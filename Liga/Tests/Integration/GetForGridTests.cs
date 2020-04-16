using System.Collections.Generic;
using System.Linq;
using LigaSoft.Controllers;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.ViewModels;
using NUnit.Framework;

namespace Tests.Integration
{
	[TestFixture]
	public class GetForGridTests : BaseIntegrationTest
	{
	    private readonly ClubController _ABMController;
		private readonly int _totalDeClubesEnLaBase;
		private readonly string _nombrePrimerClubSegunOrdenAlfabetico;
		private readonly string _nombreUltimoClubSegunOrdenAlfabetico;

		public GetForGridTests()
	    {
		    _nombrePrimerClubSegunOrdenAlfabetico = "Boca";
		    _nombreUltimoClubSegunOrdenAlfabetico = "Velez";
		    _ABMController = new ClubController();
		    _totalDeClubesEnLaBase = Context.Clubs.Count();
	    }

		[Test]
	    public void SinParametrizacionDevuelveTodosLosRegistros()
		{
			var result = _ABMController.GetForGrid(1, _totalDeClubesEnLaBase, null, null, null, null, null, null, null);
			var clubs = (List<ClubVM>) result.Data.GetReflectedProperty("records");

			Assert.AreEqual(clubs.Count, _totalDeClubesEnLaBase);
	    }

		[Test]
		public void DevuelveTodosLosRegistrosOrdenadosAlfabeticamenteAscendente()
		{
			var result = _ABMController.GetForGrid(1, _totalDeClubesEnLaBase, "Nombre", "asc", null, null, null, null, null);
			var clubs = (List<ClubVM>)result.Data.GetReflectedProperty("records");

			Assert.AreEqual(clubs.First().Nombre, _nombrePrimerClubSegunOrdenAlfabetico);
			Assert.AreEqual(clubs.Last().Nombre, _nombreUltimoClubSegunOrdenAlfabetico);
		}

		[Test]
		public void DevuelveTodosLosRegistrosOrdenadosAlfabeticamenteDescendente()
		{
			var result = _ABMController.GetForGrid(1, _totalDeClubesEnLaBase, "Nombre", "desc", null, null, null, null, null);
			var clubs = (List<ClubVM>)result.Data.GetReflectedProperty("records");

			Assert.AreEqual(clubs.First().Nombre, _nombreUltimoClubSegunOrdenAlfabetico);
			Assert.AreEqual(clubs.Last().Nombre, _nombrePrimerClubSegunOrdenAlfabetico);
		}
	}
}
