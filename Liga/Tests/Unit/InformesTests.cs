using System.Linq;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;
using Tests.Unit.Utilidades;

namespace Tests.Unit
{
	[TestFixture]
	public class InformesTests : BaseUnitTest
	{
	    private readonly InformeVMM _vmm;

	    public InformesTests()
	    {
		    _vmm = new InformeVMM(Context);
	    }

		[Test]
	    public void InformePagoCuotasPorMes_DevuelveUnRenglonPorCadaClub()
		{
			var vm = _vmm.PagoCuotasPorMesMap();
		    Assert.AreEqual(vm.Renglones.Count, Context.Clubs.Count());
	    }

		[Test]
		public void InformeCantidadDeJugadoresPorTorneo_DevuelveLaCantidadCorrectaDeJugadoresPorEquipo()
		{
			var vm = _vmm.CantidadDeJugadoresPorTorneoMap(2022);
			Assert.AreEqual(1, vm.Renglones.First().CantidadDeJugadores);
			Assert.AreEqual(2, vm.Renglones.Skip(1).First().CantidadDeJugadores);
		}
	}
}
