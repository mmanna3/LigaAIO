using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace LigaSoft.ViewModelMappers
{
	public class EliminacionDirectaVMM
	{
		public EliminacionDirectaVMM()
		{
		}

		public IList<PartidosPorCategoriaVM> MapPartidos(List<PartidoEliminacionDirecta> partidos)
		{
			var result = new List<PartidosPorCategoriaVM>();

			var partidosOrdenadosPorCategoria = partidos.GroupBy(x => x.CategoriaId);

            foreach (var partidosDeCadaCategoria in partidosOrdenadosPorCategoria)
            {
				var partidosPorCategoriaVM = new PartidosPorCategoriaVM(
					partidosDeCadaCategoria.Key, 
					partidosDeCadaCategoria.First().Categoria.Nombre, 
					MapPartido(partidosDeCadaCategoria)
				);
				result.Add(partidosPorCategoriaVM);
            }

			return result;
        }

		private IList<PartidoEliminacionDirectaVM> MapPartido(IGrouping<int, PartidoEliminacionDirecta> partidos)
		{
			var vmList = new List<PartidoEliminacionDirectaVM>();

			foreach (var partido in partidos)
			{
				var vm = new PartidoEliminacionDirectaVM
				{
					Fase = partido.Fase,
					Local = partido.Local.Nombre,
					Visitante = partido.Visitante.Nombre,
					GolesLocal = partido.GolesLocal,
					GolesVisitante = partido.GolesVisitante,
					PenalesLocal = partido.PenalesLocal,
					PenalesVisitante = partido.PenalesVisitante,
				};
				
				vmList.Add(vm);
			}

			return vmList;
		}
	}
}