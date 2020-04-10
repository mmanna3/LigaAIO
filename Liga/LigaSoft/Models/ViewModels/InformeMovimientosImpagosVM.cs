using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigaSoft.Models.ViewModels
{
	public class InformeMovimientosImpagosVM
	{		
		public string Insumos { get; set; }
		public string Fichajes { get; set; }
		public string Libres { get; set; }
		public string Cuotas { get; set; }
		public string Total { get; set; }

		[Display (Name="Clubes deudores")]
		public List<string> ClubesDeudores { get; set; }

		public int CalcularTotal()
		{
			return Convert.ToInt32(Insumos) + Convert.ToInt32(Fichajes) + Convert.ToInt32(Libres) + Convert.ToInt32(Cuotas);
		}

		public void Formatear()
		{
			Insumos = $"${Insumos}";
			Fichajes = $"${Fichajes}";
			Libres = $"${Libres}";
			Cuotas = $"${Cuotas}";
			Total = $"${Total}";
		}
	}
}