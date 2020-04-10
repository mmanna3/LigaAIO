using System;

namespace LigaSoft.Models.ViewModels
{
	public class TablaCategoriaRenglonVM
	{
		public TablaCategoriaRenglonVM()
		{
			Pts = 0;
			Pj = 0;
			Pg = 0;
			Pe = 0;
			Pp = 0;
			Np = 0;
			Gf = 0;
			Gc = 0;
			Df = 0;
		}

		public int Posicion { get; set; }
		public string Escudo { get; set; }
		public int EquipoId { get; set; }
		public string Equipo { get; set; }
		public int Pts { get; set; }
		public int Pj { get; set; }
		public int Pg { get; set; }
		public int Pe { get; set; }
		public int Pp { get; set; }
		public int Np { get; set; }
		public int Gf { get; set; }
		public int Gc { get; set; }
		public int Df { get; set; }		

		public void CalcularDiferenciaDeGol()
		{
			Df = Gf - Gc;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}