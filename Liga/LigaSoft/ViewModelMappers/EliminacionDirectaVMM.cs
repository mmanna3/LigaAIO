using LigaSoft.Migrations;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
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

		public List<PartidosPorCategoriaVM> CompletarPartidosDeTodasLasFases(FaseDeEliminacionDirectaEnum fase, IList<PartidosPorCategoriaVM> partidos)
		{
			switch (fase)
			{
				case FaseDeEliminacionDirectaEnum.Octavos:
					foreach (var categoria in partidos)
					{
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Octavos);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Cuartos);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Semifinal);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Final);
					}
					break;
				case FaseDeEliminacionDirectaEnum.Cuartos:
					foreach (var categoria in partidos)
					{
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Cuartos);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Semifinal);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Final);
					}
					break;
				case FaseDeEliminacionDirectaEnum.Semifinal:
					foreach (var categoria in partidos)
					{
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Semifinal);
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Final);
					}
					break;
				case FaseDeEliminacionDirectaEnum.Final:
					foreach (var categoria in partidos)
					{
						CompletarPartidosPorFase(categoria, FaseDeEliminacionDirectaEnum.Final);
					}
					break;
				default:
					break;
			}

			return (List<PartidosPorCategoriaVM>)partidos;
		}

		private static void CompletarPartidosPorFase(PartidosPorCategoriaVM categoria, FaseDeEliminacionDirectaEnum fase)
		{
			var cantidad = categoria.PartidosEliminacionDirecta.Where(x => x.Fase == fase).Count();
			int orden = 0;
			while (cantidad < ((int)fase/2))
			{
				categoria.PartidosEliminacionDirecta.Add(new PartidoEliminacionDirectaVM
				{
					Fase = fase,
					Orden = orden,
				});
				cantidad++;
				orden++;
			}
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
					Orden = partido.Orden
				};
				
				vmList.Add(vm);
			}

			return vmList;
		}

		public IList<PartidosPorCategoriaVM> CompletarCategorias(List<Categoria> categorias, IList<PartidosPorCategoriaVM> partidosPorCategoria)
		{
			foreach (var cat in categorias)
			{
				var categoriaConPartidos = partidosPorCategoria.SingleOrDefault(x => x.CategoriaId == cat.Id);
				if (categoriaConPartidos == null)
					partidosPorCategoria.Add(new PartidosPorCategoriaVM(cat.Id, cat.Nombre, new List<PartidoEliminacionDirectaVM>()));
			}
			return partidosPorCategoria;
		}
	}
}