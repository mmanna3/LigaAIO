using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LigaSoft.Models.ViewModels
{
	public class AgregarLeyendaVM
	{
		public int ZonaId { get; set; }
		public string Zona { get; set; }
		public string Torneo { get; set; }
		public int TorneoId { get; set; }
		
		[Display(Name = "Categoría")]
		public int CategoriaId { get; set; }
		public IList<CategoriaConLeyendaVM> CategoriasConLeyenda { get; set; }
		
		public IList<SelectListItem> Categorias { get; set; }

		[Display(Name = "Leyenda")]
		[MaxLength(255)]
		public string Leyenda { get; set; }

		public AgregarLeyendaVM()
		{ }

		public AgregarLeyendaVM(int zonaId, string zona, int torneoId, string torneo, IList<CategoriaConLeyendaVM> categorias)
		{
			ZonaId = zonaId;
			Zona = zona;
			TorneoId = torneoId;
			Torneo = torneo;
			CategoriasConLeyenda = categorias;

			Categorias = new List<SelectListItem>();
			foreach (var cat in CategoriasConLeyenda)
			{
				Categorias.Add(new SelectListItem{Text = cat.Nombre, Value = cat.Id.ToString()});
			}
		}
	}
	
	public class CategoriaConLeyendaVM
	{
		public CategoriaConLeyendaVM(int catId, string catNombre, string zonaCategoriaLeyenda)
		{
			Leyenda = zonaCategoriaLeyenda;
			Nombre = catNombre;
			Id = catId;
		}
		public string Leyenda { get; set; }
		public string Nombre { get; set; }
		public int Id { get; set; }
	}
}