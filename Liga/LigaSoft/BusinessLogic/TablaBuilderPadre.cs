using System;
using System.Collections.Generic;
using System.Linq;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.BusinessLogic
{
	//Probar si los refs son necesarios
	public abstract class TablaBuilderPadre
	{
	    protected readonly ApplicationDbContext Context;
		private static IImagenesEscudosPersistence _imagenesEscudosPersistence;

		protected TablaBuilderPadre(ApplicationDbContext context)
		{
			Context = context;
			_imagenesEscudosPersistence = new ImagenesEscudosDiskPersistence(new AppPathsWebApp());
		}

		public TablasVM Tablas(Zona zona)
	    {
		    var vm = new TablasVM { ZonaId = zona.Id, TorneoId = zona.TorneoId, Titulo = $"Tablas de {zona.DescripcionCompleta()}", VerGoles = zona.VerGolesEnTabla };

			if (zona.TieneAlMenosUnaCategoriaUnaFechaYUnEquipo())
		    {
			    CrearTablaDeCadaCategoria(zona, vm);
			    AgregarLeyendaSiLaHubiere(zona, vm);
			    ProcesarPartidosDeCadaEquipo(zona, vm);
			    DescontarPuntosSiHayQuitaDePuntos(zona, vm);
			    OrdenarTablaDeCadaCategoria(vm);
			    CompletarPosiciones(vm);
			    LlenarTablaGeneral(vm);
		    }

		    return vm;
		}

		protected virtual void DescontarPuntosSiHayQuitaDePuntos(Zona zona, TablasVM vm)
		{
			foreach (var tabla in vm.TablasPorCategoria)
				foreach (var renglon in tabla.Renglones)
				{
					var quitaDePuntos = 
						zona.QuitaDePuntos.SingleOrDefault(
							x => x.EquipoId == renglon.EquipoId &&
					 		     x.CategoriaId == tabla.CategoriaId);
					
					if (quitaDePuntos?.CantidadDePuntosDescontados != null)
							renglon.Pts -= (int) quitaDePuntos.CantidadDePuntosDescontados;
				}
		}

		protected static void LlenarTablaGeneral(TablasVM vm)
	    {
			vm.TablaGeneral = new TablaCategoriaVM();
			foreach (var renglon in vm.TablasPorCategoria.First().Renglones)
			    vm.TablaGeneral.Renglones.Add(new TablaCategoriaRenglonVM
			    {
					Escudo = renglon.Escudo,
					Equipo = renglon.Equipo,
					EquipoId = renglon.EquipoId
			    });

		    foreach (var tabla in vm.TablasPorCategoria)
				foreach (var renglon in tabla.Renglones)
				{
					var renglonDelEquipo = vm.TablaGeneral.Renglones.Single(x => x.EquipoId == renglon.EquipoId);
					renglonDelEquipo.Gc += renglon.Gc;
					renglonDelEquipo.Gf += renglon.Gf;
					renglonDelEquipo.Np += renglon.Np;
					renglonDelEquipo.Pe += renglon.Pe;
					renglonDelEquipo.Pg += renglon.Pg;
					renglonDelEquipo.Pj += renglon.Pj;
					renglonDelEquipo.Pp += renglon.Pp;
					renglonDelEquipo.Pts += renglon.Pts;
					renglonDelEquipo.CalcularDiferenciaDeGol();
				}

			vm.TablaGeneral.Ordenar();
			vm.TablaGeneral.CompletarPosiciones();
	    }

	    protected static void CrearTablaDeCadaCategoria(Zona zona, TablasVM vm)
	    {
		    foreach (var cat in zona.Torneo.Categorias)
			    vm.TablasPorCategoria.Add(new TablaCategoriaVM { CategoriaId = cat.Id, Categoria = cat.Nombre });		    
		}
	    
	    protected virtual void AgregarLeyendaSiLaHubiere(Zona zona, TablasVM vm)
	    {
		    foreach (var tabla in vm.TablasPorCategoria)
		    {
			    var zonaCategoria = zona.ZonaCategorias.SingleOrDefault(x => x.CategoriaId == tabla.CategoriaId && x.EsAnual == false);
			    tabla.Leyenda = zonaCategoria?.Leyenda;
		    }
	    }

		protected void ProcesarPartidosDeCadaEquipo(Zona zona, TablasVM vm)
		{
			var equiposQueJugaronAlMenosUnaFecha = EquiposQueJugaronAlMenosUnaFecha(zona);
			foreach (var equipo in equiposQueJugaronAlMenosUnaFecha)
			    AgregarRenglonDelEquipoALaTablaDeCadaCategoria(zona, equipo, vm);
	    }

	    protected static IEnumerable<Equipo> EquiposQueJugaronAlMenosUnaFecha(Zona zona)
	    {
		    var locales = zona.Fechas.SelectMany(x => x.Jornadas).Select(y => y.Local).Where(eq => eq != null);
		    var visitantes = zona.Fechas.SelectMany(x => x.Jornadas).Select(y => y.Visitante).Where(eq => eq != null);

			return locales.Concat(visitantes).Distinct();
	    }

		protected void AgregarRenglonDelEquipoALaTablaDeCadaCategoria(Zona zona, Equipo equipo, TablasVM vm)
	    {
		    var partidosDelEquipoEnLaZona = PartidosDelEquipoEnLaZona(zona, equipo);
		    AgregarRenglonesDelEquipoALaTablaDeCadaCategoria(equipo, vm, partidosDelEquipoEnLaZona);
	    }

		protected static void AgregarRenglonesDelEquipoALaTablaDeCadaCategoria(Equipo equipo, TablasVM vm, IQueryable<Partido> partidosDelEquipoEnLaZona)
	    {
		    foreach (var tablaCategoria in vm.TablasPorCategoria)
		    {
			    var partidosDelEquipoEnLaZonaDeEstaCategoria =
				    partidosDelEquipoEnLaZona.Where(x => x.CategoriaId == tablaCategoria.CategoriaId).ToList();

			    var renglonDelEquipo = new TablaCategoriaRenglonVM
			    {
					Escudo = _imagenesEscudosPersistence.PathRelativo(equipo.Club.Id),
					EquipoId = equipo.Id,
					Equipo = equipo.Nombre
			    };

			    foreach (var partido in partidosDelEquipoEnLaZonaDeEstaCategoria)
				    ProcesarPartido(ref renglonDelEquipo, partido, equipo);

				renglonDelEquipo.CalcularDiferenciaDeGol();
			    tablaCategoria.Renglones.Add(renglonDelEquipo);
		    }
	    }

		protected abstract IQueryable<Partido> PartidosDelEquipoEnLaZona(Zona zona, Equipo equipo);

		protected static void ProcesarPartido(ref TablaCategoriaRenglonVM renglon, Partido partido, Equipo equipo)
	    {
		    if (partido.Jornada.LocalId == equipo.Id)
			    ProcesarPartidoLocal(ref renglon, partido);
			else
				ProcesarPartidoVisitante(ref renglon, partido);
		}

	    protected static void ProcesarPartidoVisitante(ref TablaCategoriaRenglonVM renglon, Partido partido)
		{
			if (partido.GolesVisitante == "S" || partido.GolesVisitante == "P" || partido.GolesVisitante == "AR")
				return;

			renglon.Pj++;
		    if (partido.GolesVisitante == "NP")
		    {
			    renglon.Np++;
			    if (partido.GolesLocal != "NP")
				    renglon.Gc += Convert.ToInt32(partido.GolesLocal);
			}
			else
		    {
			    var golesLocalInt = 0;
				if (partido.GolesLocal != "NP" && partido.GolesLocal != "S" && partido.GolesLocal != "P" && partido.GolesLocal != "AR")
					golesLocalInt = Convert.ToInt32(partido.GolesLocal);

				var golesVisitanteInt = Convert.ToInt32(partido.GolesVisitante);
			    
				renglon.Gf += golesVisitanteInt;
			    renglon.Gc += golesLocalInt;

				if (golesLocalInt == golesVisitanteInt)
			    {
				    renglon.Pe++;
				    renglon.Pts += 2;
			    }
			    else if (golesVisitanteInt > golesLocalInt)
			    {
				    renglon.Pg++;
				    renglon.Pts += 3;
			    }
			    else
			    {
				    renglon.Pp++;
				    renglon.Pts += 1;
			    }
			}
		}

	    protected static void ProcesarPartidoLocal(ref TablaCategoriaRenglonVM renglon, Partido partido)
	    {
		    if (partido.GolesLocal == "S" || partido.GolesLocal == "P" || partido.GolesLocal == "AR")
			    return;

			renglon.Pj++;
		    if (partido.GolesLocal == "NP")
		    {
			    renglon.Np++;
			    if (partido.GolesVisitante != "NP")
					renglon.Gc += Convert.ToInt32(partido.GolesVisitante);
			}			    
		    else
		    {
			    var golesVisitanteInt = 0;				
				var golesLocalInt = Convert.ToInt32(partido.GolesLocal);

			    if (partido.GolesVisitante != "NP" && partido.GolesVisitante != "S" && partido.GolesVisitante != "P" && partido.GolesVisitante != "AR")
					golesVisitanteInt = Convert.ToInt32(partido.GolesVisitante);

			    renglon.Gf += golesLocalInt;
			    renglon.Gc += golesVisitanteInt;

			    if (golesLocalInt == golesVisitanteInt)
			    {
				    renglon.Pe++;
				    renglon.Pts += 2;
			    }
			    else if (golesLocalInt > golesVisitanteInt)
			    {
				    renglon.Pg++;
				    renglon.Pts += 3;
			    }
			    else
			    {
				    renglon.Pp++;
				    renglon.Pts += 1;
			    }
		    }
		}

	    protected static void OrdenarTablaDeCadaCategoria(TablasVM vm)
	    {
		    foreach (var tabla in vm.TablasPorCategoria)
			    tabla.Ordenar();
	    }

		private static void CompletarPosiciones(TablasVM vm)
		{
			foreach (var tabla in vm.TablasPorCategoria)
				tabla.CompletarPosiciones();
		}
	}
}