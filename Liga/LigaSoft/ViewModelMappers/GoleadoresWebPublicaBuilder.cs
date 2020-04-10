using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	internal class GoleadoresWebPublicaBuilder
	{
		private readonly ApplicationDbContext _context;

		public GoleadoresWebPublicaBuilder(ApplicationDbContext context)
		{
			_context = context;
		}

		public GoleadoresVM Tablas(Zona zona)
		{
			var result = new GoleadoresVM($"Goleadores de la zona {zona.Nombre}");			

			var partidosDeLaZona = _context.Partidos.Where(x => x.Jornada.Fecha.ZonaId == zona.Id);			
			var categoriasDeLosGoleadores = zona.Torneo.Categorias;

			foreach (var categoria in categoriasDeLosGoleadores)
			{
				var goleadoresPorCategoriaVM = new GoleadoresCategoriaVM($"{categoria.Nombre}");
				var partidosDeLaCategoriaEnLaZonaIds = partidosDeLaZona.Where(x => x.CategoriaId == categoria.Id).Select(x => x.Id);
				var goleadoresDePartidosDeLaZona = _context.Goleadores.Where(x => partidosDeLaCategoriaEnLaZonaIds.Contains(x.PartidoId)).ToList();

				var renglones = goleadoresDePartidosDeLaZona
											.GroupBy(x => x.Jugador)
											.Select(x => new RenglonGoleadorVM
											{
												Jugador = $"{x.FirstOrDefault()?.Jugador.Apellido.ToCamelCase()}, {x.FirstOrDefault()?.Jugador.Nombre.ToCamelCase()}",
												Equipo = x.FirstOrDefault()?.Equipo.Nombre,
												Goles = x.Sum(y => y.Cantidad)
											})
											.Take(10)
											.OrderByDescending(x => x.Goles)
											.ToList();				

				goleadoresPorCategoriaVM.Renglones.AddRange(renglones);
				result.GoleadoresPorCategoria.Add(goleadoresPorCategoriaVM);
			}

			return result;
		}
	}
}