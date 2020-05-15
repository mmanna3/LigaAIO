using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;

namespace Tests.Unit.Utilidades
{
	internal sealed class DropCreateDatabaseAlwaysAndSeed : DropCreateDatabaseAlways<ApplicationDbContext>
	{
		public override void InitializeDatabase(ApplicationDbContext context)
		{
			// This tells the database to close all connections and rolback open transactions.
			// The intent to avoid any open database connections errors during database drop.
			if (context.Database.Exists())
			{
				context.Database.ExecuteSqlCommand(
					TransactionalBehavior.DoNotEnsureTransaction,
					$"ALTER DATABASE [{context.Database.Connection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
			}

			base.InitializeDatabase(context);
		}

		private List<Equipo> _equipos;

		protected override void Seed(ApplicationDbContext context)
		{
			var torneo1 = new Torneo
			{
				Anio = Anio.A2019,
				Tipo = context.TorneoTipos.Single(x => x.Descripcion == "Vespertino"),
				Publico = true
			};

			var torneo2 = new Torneo
			{
				Anio = Anio.A2020,
				Tipo = context.TorneoTipos.Single(x => x.Descripcion == "Vespertino"),
				Publico = true
			};

			var categoriaPrimera = new Categoria
			{
				Nombre = "Primera",
				Orden = 1,
				Torneo = torneo1
			};

			var categoriaSegunda = new Categoria
			{
				Nombre = "Segunda",
				Orden = 2,
				Torneo = torneo1
			};

			var zonaRelampago = new Zona
			{
				Tipo = ZonaTipo.Relampago,
				Nombre = "Relámpago",
				Torneo = torneo1,
				FixturePublicado = true,
				VerGolesEnTabla = true
			};

			var zonaAperturaA = new Zona
			{
				Tipo = ZonaTipo.Apertura,
				Nombre = "A",
				Torneo = torneo1,
				FixturePublicado = true,
				VerGolesEnTabla = true
			};

			var zonaClausuraA = new Zona
			{
				Tipo = ZonaTipo.Clausura,
				Nombre = "A",
				Torneo = torneo1,
				FixturePublicado = true,
				VerGolesEnTabla = true
			};

			var zonaClausuraB = new Zona
			{
				Tipo = ZonaTipo.Clausura,
				Nombre = "B",
				Torneo = torneo1,
				FixturePublicado = true,
				VerGolesEnTabla = true
			};

			var clubes = new List<Club> { new Club { Nombre = "Boca", Techo = true }, new Club { Nombre = "River" }, new Club { Nombre = "Independiente" }, new Club { Nombre = "Racing" }, new Club { Nombre = "Velez" }, new Club { Nombre = "San Lorenzo" }, new Club { Nombre = "Huracán" } };
			_equipos = new List<Equipo>
			{
				new Equipo { Nombre = "Boca", Club = clubes.Single(x => x.Nombre == "Boca"), Torneo = torneo1, Zona = zonaClausuraA},
				new Equipo { Nombre = "River", Club = clubes.Single(x => x.Nombre == "River"), Torneo = null, Zona = null},
				new Equipo { Nombre = "Independiente", Club = clubes.Single(x => x.Nombre == "Independiente"), Torneo = torneo1, Zona = zonaClausuraB},
				new Equipo { Nombre = "Racing", Club = clubes.Single(x => x.Nombre == "Racing"), Torneo = torneo1, Zona = null},
				new Equipo { Nombre = "San Lorenzo", Club = clubes.Single(x => x.Nombre == "San Lorenzo"), Torneo = torneo1, Zona = zonaClausuraB},
				new Equipo { Nombre = "Velez", Club = clubes.Single(x => x.Nombre == "Velez"), Torneo = torneo1, Zona = zonaClausuraB},
				new Equipo { Nombre = "Huracán", Club = clubes.Single(x => x.Nombre == "Huracán"), Torneo = torneo1, Zona = zonaClausuraB}
			};

						
			var fechasAperturaZonaA = Generar3Fechas(zonaAperturaA);
			var jornadasZonaAperturaA = Generar2JornadasPorFechaParaZonaApertura(fechasAperturaZonaA);
			var partidosCategoriaPrimeraAperturaA = GenerarPartidosDeLaCategoriaPrimeraZonaAperturaA(jornadasZonaAperturaA, categoriaPrimera);
			var partidosCategoriaSegundaAperturaA = GenerarPartidosDeLaCategoriaSegundaZonaAperturaA(jornadasZonaAperturaA, categoriaSegunda);

			var fechasClausuraZonaA = Generar3Fechas(zonaClausuraA);
			var jornadasZonaClausuraA = Generar1JornadaPorFechaParaZonaClausuraA(fechasClausuraZonaA);
			var partidosCategoriaPrimeraClausuraA = GenerarPartidosDeLaCategoriaPrimeraZonaClausuraA(jornadasZonaClausuraA, categoriaPrimera);
			var partidosCategoriaSegundaClausuraA = GenerarPartidosDeLaCategoriaSegundaZonaClausuraA(jornadasZonaClausuraA, categoriaSegunda);

			var fechasClausuraZonaB = Generar3Fechas(zonaClausuraB);
			var jornadasZonaClausuraB = Generar2JornadaPorFechaParaZonaClausuraB(fechasClausuraZonaB);
			var partidosCategoriaPrimeraClausuraB = GenerarPartidosDeLaCategoriaPrimeraZonaClausuraB(jornadasZonaClausuraB, categoriaPrimera);
			var partidosCategoriaSegundaClausuraB = GenerarPartidosDeLaCategoriaSegundaZonaClausuraB(jornadasZonaClausuraB, categoriaSegunda);


			context.Torneos.Add(torneo1);
			context.Torneos.Add(torneo2);

			context.Categorias.Add(categoriaPrimera);
			context.Categorias.Add(categoriaSegunda);

			context.Zonas.Add(zonaAperturaA);
			context.Zonas.Add(zonaClausuraA);
			context.Zonas.Add(zonaClausuraB);
			context.Zonas.Add(zonaRelampago);

			context.Clubs.AddRange(clubes);
			context.Equipos.AddRange(_equipos);

			context.Fechas.AddRange(fechasAperturaZonaA);
			context.Fechas.AddRange(fechasClausuraZonaA);
			context.Fechas.AddRange(fechasClausuraZonaB);

			context.Jornadas.AddRange(jornadasZonaAperturaA);
			context.Jornadas.AddRange(jornadasZonaClausuraA);
			context.Jornadas.AddRange(jornadasZonaClausuraB);

			context.Partidos.AddRange(partidosCategoriaPrimeraAperturaA);
			context.Partidos.AddRange(partidosCategoriaSegundaAperturaA);

			context.Partidos.AddRange(partidosCategoriaPrimeraClausuraA);
			context.Partidos.AddRange(partidosCategoriaSegundaClausuraA);

			context.Partidos.AddRange(partidosCategoriaPrimeraClausuraB);
			context.Partidos.AddRange(partidosCategoriaSegundaClausuraB);
		}

		private IEnumerable<Partido> GenerarPartidosDeLaCategoriaSegundaZonaClausuraB(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "3", "3"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "2", "1"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "2", "4"),
				GenerarPartido(jornadas.Skip(3).First(), categoria, "1", "0"),
				GenerarPartido(jornadas.Skip(4).First(), categoria, "2", "4"),
				GenerarPartido(jornadas.Skip(5).First(), categoria, "NP", "2"),
			};
		}

		private IEnumerable<Partido> GenerarPartidosDeLaCategoriaPrimeraZonaClausuraB(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "3", "0"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "3", "3"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "2", "2"),
				GenerarPartido(jornadas.Skip(3).First(), categoria, "1", "2"),
				GenerarPartido(jornadas.Skip(4).First(), categoria, "S", "S"),
				GenerarPartido(jornadas.Skip(5).First(), categoria, "NP", "2"),
			};
		}


		private static IEnumerable<Partido> GenerarPartidosDeLaCategoriaPrimeraZonaAperturaA(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "3", "0"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "1", "1"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "2", "1"),
				GenerarPartido(jornadas.Skip(3).First(), categoria, "1", "0"),
				GenerarPartido(jornadas.Skip(4).First(), categoria, "2", "1"),
				GenerarPartido(jornadas.Skip(5).First(), categoria, "NP", "2"),
			};
		}

		private static IEnumerable<Partido> GenerarPartidosDeLaCategoriaSegundaZonaAperturaA(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "3", "12"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "4", "0"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "5", "3"),
				GenerarPartido(jornadas.Skip(3).First(), categoria, "P", "P"),
				GenerarPartido(jornadas.Skip(4).First(), categoria, "S", "S"),
				GenerarPartido(jornadas.Skip(5).First(), categoria, "3", "1"),
			};
		}

		private IEnumerable<Partido> GenerarPartidosDeLaCategoriaSegundaZonaClausuraA(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "3", "0"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "S", "S"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "5", "3"),
			};
		}

		private IEnumerable<Partido> GenerarPartidosDeLaCategoriaPrimeraZonaClausuraA(List<Jornada> jornadas, Categoria categoria)
		{
			return new List<Partido>
			{
				GenerarPartido(jornadas.First(), categoria, "2", "1"),
				GenerarPartido(jornadas.Skip(1).First(), categoria, "0", "0"),
				GenerarPartido(jornadas.Skip(2).First(), categoria, "1", "3"),
			};
		}

		private static Partido GenerarPartido(Jornada jornada, Categoria categoria, string golesLocal, string golesVisitante)
		{
			return new Partido
			{
				Categoria = categoria,
				Jornada = jornada,
				GolesLocal = golesLocal,
				GolesVisitante = golesVisitante
			};
		}

		private List<Jornada> Generar2JornadasPorFechaParaZonaApertura(List<Fecha> fechas)
		{
			return new List<Jornada>
			{
				GenerarJornada(fechas.Single(x => x.Numero == 1) , _equipos.Single(x => x.Nombre.Equals("Boca")), _equipos.Single(x => x.Nombre.Equals("River"))),
				GenerarJornada(fechas.Single(x => x.Numero == 1), _equipos.Single(x => x.Nombre.Equals("Independiente")), _equipos.Single(x => x.Nombre.Equals("Racing"))),

				GenerarJornada(fechas.Single(x => x.Numero == 2), _equipos.Single(x => x.Nombre.Equals("Boca")), _equipos.Single(x => x.Nombre.Equals("Independiente"))),
				GenerarJornada(fechas.Single(x => x.Numero == 2), _equipos.Single(x => x.Nombre.Equals("Racing")), _equipos.Single(x => x.Nombre.Equals("River"))),

				GenerarJornada(fechas.Single(x => x.Numero == 3), _equipos.Single(x => x.Nombre.Equals("Boca")), _equipos.Single(x => x.Nombre.Equals("Racing"))),
				GenerarJornada(fechas.Single(x => x.Numero == 3), _equipos.Single(x => x.Nombre.Equals("River")), _equipos.Single(x => x.Nombre.Equals("Independiente")))
			};			
		}

		private List<Jornada> Generar1JornadaPorFechaParaZonaClausuraA(List<Fecha> fechas)
		{
			return new List<Jornada>
			{
				GenerarJornada(fechas.Single(x => x.Numero == 1) , _equipos.Single(x => x.Nombre.Equals("Boca")), _equipos.Single(x => x.Nombre.Equals("River"))),
				GenerarJornada(fechas.Single(x => x.Numero == 2), _equipos.Single(x => x.Nombre.Equals("River")), _equipos.Single(x => x.Nombre.Equals("Boca"))),
				GenerarJornada(fechas.Single(x => x.Numero == 3), _equipos.Single(x => x.Nombre.Equals("Boca")), _equipos.Single(x => x.Nombre.Equals("River")))
			};
		}

		private List<Jornada> Generar2JornadaPorFechaParaZonaClausuraB(List<Fecha> fechas) //éste
		{
			return new List<Jornada>
			{
				GenerarJornada(fechas.Single(x => x.Numero == 1) , _equipos.Single(x => x.Nombre.Equals("Velez")), _equipos.Single(x => x.Nombre.Equals("San Lorenzo"))),
				GenerarJornada(fechas.Single(x => x.Numero == 1), _equipos.Single(x => x.Nombre.Equals("Independiente")), _equipos.Single(x => x.Nombre.Equals("Huracán"))),

				GenerarJornada(fechas.Single(x => x.Numero == 2), _equipos.Single(x => x.Nombre.Equals("San Lorenzo")), _equipos.Single(x => x.Nombre.Equals("Independiente"))),
				GenerarJornada(fechas.Single(x => x.Numero == 2), _equipos.Single(x => x.Nombre.Equals("Huracán")), _equipos.Single(x => x.Nombre.Equals("Velez"))),

				GenerarJornada(fechas.Single(x => x.Numero == 3), _equipos.Single(x => x.Nombre.Equals("San Lorenzo")), _equipos.Single(x => x.Nombre.Equals("Huracán"))),
				GenerarJornada(fechas.Single(x => x.Numero == 3), _equipos.Single(x => x.Nombre.Equals("Velez")), _equipos.Single(x => x.Nombre.Equals("Independiente")))
			};
		}

		private Jornada GenerarJornada(Fecha fecha, Equipo local, Equipo visitante)
		{
			return new Jornada
			{
				Fecha = fecha,
				Local = local,
				Visitante = visitante,
				ResultadosVerificados = true
			};
		}

		private static List<Fecha> Generar3Fechas(Zona zona)
		{
			return new List<Fecha>
			{
				GenerarFecha(zona, 1), GenerarFecha(zona, 2), GenerarFecha(zona, 3)
			};
		}

		private static Fecha GenerarFecha(Zona zona, int numero)
		{
			return new Fecha
			{
				DiaDeLaFecha = DateTime.Now.AddDays(-10 + numero),
				Numero = numero,
				Zona = zona,
				Publicada = true
			};
		}
	}
}
