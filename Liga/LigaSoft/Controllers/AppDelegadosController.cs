using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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

		public AppDelegadosController()
		{
			_context = new ApplicationDbContext();
			_jugadorVMM = new JugadorVMM(_context);
		}

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
			var jugadores = _context.JugadorEquipos.Where(x => x.EquipoId == equipoId).Select(x => x.Jugador).ToList();

			var resultado = new List<JugadorCarnetVM>();

			foreach (var jugador in jugadores)
			{
				resultado.Add(_jugadorVMM.MapJugadorParaCarnet(jugador, equipo));
			}
					
			return JsonConvert.SerializeObject(ApiResponseCreator.Exito(resultado));
		}
	}
}