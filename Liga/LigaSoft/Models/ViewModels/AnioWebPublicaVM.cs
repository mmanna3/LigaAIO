using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class AnioWebPublicaVM
	{
		public int Anio { get; set; }

		[Display(Name = "Torneos")]
		public IList<TorneoWebPublicaVM> Torneos { get; set; }

		public AnioWebPublicaVM()
		{
			Torneos = new List<TorneoWebPublicaVM>();
		}
	}
}