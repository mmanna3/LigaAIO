using System.Collections.Generic;
using System.Linq;

namespace LigaSoft.Models.ViewModels
{
	public class TablaCategoriaVM
	{
		public int CategoriaId { get; set; }
		public string Categoria { get; set; }
		public List<TablaCategoriaRenglonVM> Renglones { get; set; }

		public TablaCategoriaVM()
		{
			Renglones = new List<TablaCategoriaRenglonVM>();
		}

		public void Ordenar()
		{
			Renglones = new List<TablaCategoriaRenglonVM>(Renglones.OrderBy(item => item, new RenglonesComparerVM()));
		}

		public void CompletarPosiciones()
		{
			var posicion = 1;
			foreach (var renglon in Renglones)
			{
				renglon.Posicion = posicion;
				posicion++;
			}
		}
	}

	public class RenglonesComparerVM : IComparer<TablaCategoriaRenglonVM>
	{
		public int Compare(TablaCategoriaRenglonVM x, TablaCategoriaRenglonVM y)
		{
			if (x.Pts != y.Pts)
				return y.Pts.CompareTo(x.Pts);
			if (x.Df != y.Df)
				return y.Df.CompareTo(x.Df);
			return y.Gf.CompareTo(x.Gf);
		}
	}
}