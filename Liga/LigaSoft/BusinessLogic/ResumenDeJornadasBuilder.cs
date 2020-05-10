using System;
using System.Collections.Generic;
using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.BusinessLogic
{
    public class ResumenDeJornadasBuilder
	{
		private static IImagenesEscudosPersistence _imagenesEscudosPersistence;

		public ResumenDeJornadasBuilder()
		{
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		}

		public ResumenDeJornadasVM Tablas(Zona zona, List<Fecha> fechas)
		{
			var result = new ResumenDeJornadasVM($"Resumen de jornadas {zona.DescripcionCompleta()}", zona.TorneoId, zona.Id);
			AgregarCategorias(zona, result);									
			CrearUnaTablaPorFecha(fechas, result);

			foreach (var fecha in fechas)
			{
				var jornadasContador = 1;
				foreach (var jornada in fecha.Jornadas)
				{
					var renglonLocal = new JornadasPorFechaRenglonVM
					{
						JornadaId = jornada.Id,
						JornadaNumero = jornadasContador,
						Equipo = jornada.NombreDelLocal(),
						Escudo = _imagenesEscudosPersistence.PathRelativo(jornada.Local?.ClubId ?? -1),
					};

					var renglonVisitante = new JornadasPorFechaRenglonVM
					{
						JornadaId = jornada.Id,
						JornadaNumero = jornadasContador,
						Equipo = jornada.NombreDelVisitante(),
						Escudo = _imagenesEscudosPersistence.PathRelativo(jornada.Visitante?.ClubId ?? -1),
					};

					foreach (var partido in jornada.Partidos)
					{
						var resultadoPorCatLocal = new ResultadosPorCategoriaVM
						{
							Orden = partido.Categoria.Orden,
							Goles = partido.GolesLocal
						};

						var resultadoPorCatVisit = new ResultadosPorCategoriaVM
						{
							Orden = partido.Categoria.Orden,
							Goles = partido.GolesVisitante
						};

						renglonLocal.ResultadosPorCategorias.Add(resultadoPorCatLocal);
						renglonVisitante.ResultadosPorCategorias.Add(resultadoPorCatVisit);						
					}

					renglonLocal.ResultadosPorCategorias.Sort((x, y) => x.Orden.CompareTo(y.Orden));
					renglonVisitante.ResultadosPorCategorias.Sort((x, y) => x.Orden.CompareTo(y.Orden));

					CalcularPuntosTotales(renglonLocal, renglonVisitante);

					renglonLocal.PartidoVerificado = jornada.ResultadosVerificados.ToCheckString();
					renglonVisitante.PartidoVerificado = jornada.ResultadosVerificados.ToCheckString();

					result.JornadasPorFecha.Single(x => x.FechaId == fecha.Id).Renglones.Add(renglonLocal);					
					result.JornadasPorFecha.Single(x => x.FechaId == fecha.Id).Renglones.Add(renglonVisitante);					
					jornadasContador++;
				}				
			}

			return result;
		}

		private static void CalcularPuntosTotales(JornadasPorFechaRenglonVM renglonLocal, JornadasPorFechaRenglonVM renglonVisitante)
		{
			foreach (var resultadoLocal in renglonLocal.ResultadosPorCategorias)
			{
				var resultadoVisitante = renglonVisitante.ResultadosPorCategorias.Single(x => x.Orden == resultadoLocal.Orden);
				switch (resultadoLocal.Goles)
				{
					case "S":
					case "P":
						continue;
					default:
						renglonLocal.PartidosJugados++;
						renglonVisitante.PartidosJugados++;
						break;
				}

				if (resultadoLocal.Goles == "NP" && resultadoVisitante.Goles == "NP")
					continue;

				var golesLocalInt = 0;
				var golesVisitInt = 0;

				if (resultadoVisitante.Goles != "NP")
					golesVisitInt = Convert.ToInt32(resultadoVisitante.Goles);

				if (resultadoLocal.Goles != "NP")
					golesLocalInt = Convert.ToInt32(resultadoLocal.Goles);

				if (golesLocalInt > golesVisitInt)
				{
					renglonLocal.PuntosTotales += 3;
					if (resultadoVisitante.Goles != "NP")
						renglonVisitante.PuntosTotales += 1;
				}
				else if (golesLocalInt < golesVisitInt)
				{
					renglonVisitante.PuntosTotales += 3;
					if (resultadoLocal.Goles != "NP")
						renglonLocal.PuntosTotales += 1;
				}
				else if (golesLocalInt == golesVisitInt)
				{
					renglonLocal.PuntosTotales += 2;
					renglonVisitante.PuntosTotales += 2;
				}
			}
		}

		private static void AgregarCategorias(Zona zona, ResumenDeJornadasVM result)
		{
			foreach (var categoria in zona.Torneo.Categorias)
				result.Categorias.Add(new CategoriaVM {Nombre = categoria.Nombre, Id = categoria.Id, Orden = categoria.Orden});
			result.Categorias.Sort((x, y) => x.Orden.CompareTo(y.Orden));
		}

		private static void CrearUnaTablaPorFecha(IEnumerable<Fecha> fechas, ResumenDeJornadasVM result)
		{
			foreach (var fecha in fechas)
				result.JornadasPorFecha.Add(new JornadasPorFechaVM(fecha.Id, fecha.Numero, fecha.Publicada));
		}
	}
}
