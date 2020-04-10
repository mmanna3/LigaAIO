using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Attributes;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class ClubVM : ViewModelConId
	{
		[YKNRequired]
		[Display(Name = "Nombre del club")]
		public string Nombre { get; set; }

		public string Localidad { get; set; }

		[Display(Name = "Dirección")]
		public string Direccion { get; set; }

		[Display(Name = "Techo")]
		public Techo Techo { get; set; } 

		[Display(Name = "Equipos")]
		public IEnumerable<string> Equipos { get; set; }

		[Display(Name = "Valor total de la cuota")]
		public string Cuota { get; set; }

		public string SaldoDeudor { get; set; }

		public bool? TechoEnumToNullableBool()
		{
			switch (Techo)
			{
				case Techo.Si:
					return true;
				case Techo.No:
					return false;
				default:
					return null;
			}
		}

		public ConceptoTotales ConceptoTotales { get; set; }

		public string Escudo { get; set; }
	}

	public class ConceptoTotales
	{
		public string DeudaInsumos { get; set; }
		public string DeudaCuotas { get; set; }
		public string DeudaFichajes { get; set; }
		public string DeudaLibres { get; set; }
		public string DeudaTotal { get; set; }
	}
}
