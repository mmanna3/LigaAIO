using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Newtonsoft.Json;
using LigaSoft.Models;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.CualquierEmpleadoDeLaLiga)]
	public class AppDelegadosController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly JugadorVMM _jugadorVMM;
		private readonly JugadorAutofichadoVMM _jugadorAutofichadoVMM;

		public AppDelegadosController()
		{
			_context = new ApplicationDbContext();
			_jugadorVMM = new JugadorVMM(_context);
			_jugadorAutofichadoVMM = new JugadorAutofichadoVMM(_context);
		}
		
		// protected override void OnActionExecuted(ActionExecutedContext filterContext)
		// {
		// 	if (filterContext.Exception != null)
		// 	{
		// 		Log.Error("Excepción no controlada en AppDelegadosController: " + filterContext.Exception.Message +
		// 		          ". Stack trace: " + filterContext.Exception.StackTrace, 
		// 			filterContext.Exception);
		// 	}
		// }

		[AllowAnonymous]
		public string Getjugadores(string codigoAlfanumerico) //TODO: Pedir token acá
		{
			int equipoId;
			try
			{
				equipoId = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos(codigoAlfanumerico);
			} catch (Exception e)
			{
				return JsonConvert.SerializeObject(ApiResponseCreator.Error(e.Message));
			}

			var equipo = _context.Equipos.Find(equipoId);
			var jugadores = _context.JugadorEquipos.Where(x => x.EquipoId == equipoId).Select(x => x.Jugador).OrderByDescending(x => x.FechaNacimiento).ToList();

			var resultado = new List<JugadorCarnetVM>();

			foreach (var jugador in jugadores)
			{
				resultado.Add(_jugadorVMM.MapJugadorParaCarnet(jugador, equipo));
			}
					
			return JsonConvert.SerializeObject(ApiResponseCreator.Exito(resultado));
		}
		
		[AllowAnonymous]
		public string GetPlanillas(string codigoAlfanumerico) //TODO: Pedir token acá
		{
			int equipoId;
			try
			{
				equipoId = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos(codigoAlfanumerico);
			} catch (Exception e)
			{
				return JsonConvert.SerializeObject(ApiResponseCreator.Error(e.Message));
			}

			var equipo = _context.Equipos.Find(equipoId);

			var categorias = _context.Categorias.Where(x => x.TorneoId == equipo.TorneoId);
			
			var jugadores = _context.JugadorEquipos.Where(x => x.EquipoId == equipoId).Select(x => x.Jugador).ToList();

			var resultado = new PlanillaDeJuegoVM
			{
				Planillas = new List<PlanillaDeJuegoPorCategoriaVM>(),
				Equipo = equipo.Nombre,
				Torneo = equipo.Torneo.Tipo.Descripcion
			};

			foreach (var categoria in categorias)
			{
				resultado.Planillas.Add(new PlanillaDeJuegoPorCategoriaVM
				{
					Categoria = categoria.Nombre,
					Jugadores = null
				});
			}
					
			return JsonConvert.SerializeObject(ApiResponseCreator.Exito(resultado));
		}

		[AllowAnonymous]
		public string GetJugadoresAutofichadosConEstado(int clubId) //TODO: Pedir token acá
		{
			var club = _context.Clubs.Include(club1 => club1.Equipos).SingleOrDefault(x => x.Id == clubId);
			if (club == null)
				return JsonConvert.SerializeObject(ApiResponseCreator.Error("El club no existe"));

			var equiposDelClub = club.Equipos.Select(x => x.Id);
			var jugadores = 
				_context.JugadoresaAutofichados
					.Where(x => equiposDelClub.Contains(x.EquipoId))
					.OrderByDescending(x => x.Estado)
					.ThenByDescending(x => x.Equipo.Nombre)
					.ToList();

			var resultado = new List<JugadorAutofichadoBaseVM>();

			foreach (var jugador in jugadores) 
				resultado.Add(_jugadorAutofichadoVMM.MapForBaseDetails(jugador));

			return JsonConvert.SerializeObject(ApiResponseCreator.Exito(resultado));
		}
	}
}