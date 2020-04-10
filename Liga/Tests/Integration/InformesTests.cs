using System.Linq;
using LigaSoft.Models.Dominio;
using LigaSoft.ViewModelMappers;
using NUnit.Framework;

namespace Tests.Integration
{
	[TestFixture]
	public class InformesTests : BaseIntegrationTest
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
	}
}
