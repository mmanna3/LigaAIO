using System;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;

namespace LigaSoft.Models.ViewModels
{
	public class InformeVM
	{
		public InformeVM()
		{
			Insumos = new ConceptoConFormaDePago();
			Fichajes = new ConceptoConFormaDePago();
			Libres = new ConceptoConFormaDePago();
			Cuotas = new ConceptoConFormaDePago();
			CajaEdefiIngresos = new ConceptoConFormaDePago();
			CajaEdefiEgresos = new ConceptoConFormaDePago();
			Total = new ConceptoConFormaDePago();
		}
		
		[YKNDateTime, Display(Name= "Fecha de inicio")]
		public string FechaInicio { get; set; }

		[YKNDateTime, Display(Name = "Fecha de fin")]
		public string FechaFin { get; set; }

		public ConceptoConFormaDePago Insumos { get; set; }
		public ConceptoConFormaDePago Fichajes { get; set; }
		public ConceptoConFormaDePago Libres { get; set; }
		public ConceptoConFormaDePago Cuotas { get; set; }
		public ConceptoConFormaDePago CajaEdefiIngresos { get; set; }
		public ConceptoConFormaDePago CajaEdefiEgresos { get; set; }
		public ConceptoConFormaDePago Total { get; set; }

		public void CalcularTotales()
		{
			Total.Efectivo = (Convert.ToInt32(Insumos.Efectivo) + Convert.ToInt32(Fichajes.Efectivo) + Convert.ToInt32(Libres.Efectivo) + Convert.ToInt32(Cuotas.Efectivo) + Convert.ToInt32(CajaEdefiIngresos.Efectivo) - Convert.ToInt32(CajaEdefiEgresos.Efectivo)).ToString();
			Total.Virtual = (Convert.ToInt32(Insumos.Virtual) + Convert.ToInt32(Fichajes.Virtual) + Convert.ToInt32(Libres.Virtual) + Convert.ToInt32(Cuotas.Virtual) + Convert.ToInt32(CajaEdefiIngresos.Virtual) - Convert.ToInt32(CajaEdefiEgresos.Virtual)).ToString();
			Total.CalcularTotal();

			Insumos.CalcularTotal();
			Fichajes.CalcularTotal();
			Libres.CalcularTotal();
			Cuotas.CalcularTotal();
			CajaEdefiIngresos.CalcularTotal();
			CajaEdefiEgresos.CalcularTotal();
		}

		public void Formatear()
		{
			Insumos.Formatear();
			Fichajes.Formatear();
			Libres.Formatear();
			Cuotas.Formatear();
			CajaEdefiIngresos.Formatear();
			CajaEdefiEgresos.Formatear();
			Total.Formatear();
		}
	}
	
	public class ConceptoConFormaDePago
	{
		public string Efectivo { get; set; }
		public string Virtual { get; set; }
		public string Total { get; set; }
		
		public void Formatear()
		{
			Efectivo = $"${Efectivo}";
			Virtual = $"${Virtual}";
			Total = $"${Total}";
		}

		public void CalcularTotal()
		{
			Total = (Convert.ToInt32(Efectivo) + Convert.ToInt32(Virtual)).ToString();
		}
	}
}