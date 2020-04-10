using System;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class InformeVM
	{		
		[YKNDateTime, Display(Name= "Fecha de inicio")]
		public string FechaInicio { get; set; }

		[YKNDateTime, Display(Name = "Fecha de fin")]
		public string FechaFin { get; set; }

		public string Insumos { get; set; }
		public string Fichajes { get; set; }
		public string Libres { get; set; }
		public string Cuotas { get; set; }
		public string CajaEdefiIngresos { get; set; }
		public string CajaEdefiEgresos { get; set; }
		public string Total { get; set; }

		public int CalcularTotal()
		{
			return Convert.ToInt32(Insumos) + Convert.ToInt32(Fichajes) + Convert.ToInt32(Libres) + Convert.ToInt32(Cuotas) + Convert.ToInt32(CajaEdefiIngresos) - Convert.ToInt32(CajaEdefiEgresos);
		}

		public void Formatear()
		{
			Insumos = $"${Insumos}";
			Fichajes = $"${Fichajes}";
			Libres = $"${Libres}";
			Cuotas = $"${Cuotas}";
			CajaEdefiIngresos = $"${CajaEdefiIngresos}";
			CajaEdefiEgresos = $"-${CajaEdefiEgresos}";
			Total = $"${Total}";
		}
	}
}