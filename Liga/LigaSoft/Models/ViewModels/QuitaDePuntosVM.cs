using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LigaSoft.Models.ViewModels
{
	public class QuitaDePuntosVM
	{
		public int ZonaId { get; set; }
		public string Zona { get; set; }
		
		[Display(Name = "Equipo")]
		public int EquipoId { get; set; }

		public string Torneo { get; set; }
		public int TorneoId { get; set; }
		
		public IList<QuitaPorCategoriaVM> QuitaPorCategorias { get; set; }
		public IList<EquipoCategoriaQuitaVM> EquiposConQuitaDePuntos { get; set; }
		public IList<IdDescripcionVM> Categorias { get; set; }
		public IList<SelectListItem> Equipos { get; set; }

		public QuitaDePuntosVM()
		{ }

		public QuitaDePuntosVM(int zonaId, string zona, int torneoId, string torneo, IList<QuitaPorCategoriaVM> quitaPorCategoria,
			IList<SelectListItem> equipos, List<EquipoCategoriaQuitaVM> tuplas)
		{
			ZonaId = zonaId;
			Zona = zona;
			TorneoId = torneoId;
			Torneo = torneo;
			QuitaPorCategorias = quitaPorCategoria;
			Equipos = equipos;
			EquiposConQuitaDePuntos = tuplas;
			
			// EquipoCategoriaQuitaVms = new List<EquipoCategoriaQuitaVM>();
			// foreach (var cat in categorias)
			// {
			// 	foreach (var equipoId in equipos)
			// 	{
			// 		EquipoCategoriaQuitaVms.Add(new SelectListItem{Text = cat.Nombre, Value = cat.Id.ToString()});	
			// 	}
			// 	
			// }
		}
	}
	
	public class EquipoCategoriaQuitaVM
	{
		public EquipoCategoriaQuitaVM(int catId, int equipoId, int? quitaDePuntos)
		{
			CategoriaId = catId;
			EquipoId = equipoId;
			QuitaDePuntos = quitaDePuntos;
		}
		public int CategoriaId { get; set; }
		public int EquipoId { get; set; }
		public int? QuitaDePuntos { get; set; }
	}
	
	public class QuitaPorCategoriaVM
	{
		public QuitaPorCategoriaVM()
		{
		}
		
		public QuitaPorCategoriaVM(string desc, int catId, int? quitaDePuntos)
		{
			CategoriaId = catId;
			CategoriaDesc = desc;
			QuitaDePuntos = quitaDePuntos;
		}

		[Display(Name = "Categoría")]
		public string CategoriaDesc { get; set; }
		public int CategoriaId { get; set; }
		
		[Display(Name = "Quita de puntos")]
		public int? QuitaDePuntos { get; set; }
	}
}