using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LigaSoft.Models.Enums;

namespace LigaSoft.Models.ViewModels
{
	public class TorneoWebPublicaVM
	{
		public int Id { get; set; }

		public string TipoDesc { get; set; }

		[Display(Name = "Zonas")]
		public IList<ZonaVM> Zonas { get; set; }

		public TorneoFormato Fomato { get; set; }
	}
}