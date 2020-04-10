using System.Collections.Generic;

namespace LigaSoft.Models.ViewModels
{
	public class InformePagoCuotasPorMesVM
	{
		public InformePagoCuotasPorMesVM()
		{
			Renglones = new List<ClubDeudaCuotaPorMesRenglonVM>();
		}

		public List<ClubDeudaCuotaPorMesRenglonVM> Renglones { get; set; }

		public void OrdenarAlfabeticamentePorNombreDeClub()
		{
			Renglones.Sort((x, y) => x.ClubNombre.CompareTo(y.ClubNombre));
		}
	}

	public class ClubDeudaCuotaPorMesRenglonVM
	{
		public int Id { get; set; }
		public string ValorCuota { get; set; }
		public string ClubLink { get; set; }
		public string PagoAbril { get; set; }
		public string PagoMayo { get; set; }
		public string PagoJunio { get; set; }
		public string PagoJulio { get; set; }
		public string PagoAgosto { get; set; }
		public string PagoSeptiembre { get; set; }
		public string PagoOctubre { get; set; }
		public string PagoNoviembre { get; set; }
		public string ClubNombre { get; set; }
	}
}