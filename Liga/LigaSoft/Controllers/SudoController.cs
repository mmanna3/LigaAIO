using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class SudoController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresPersistence;

		public SudoController()
		{
			_imagenesJugadoresPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
			_context = new ApplicationDbContext();
		}

		public JsonResult EliminarJugadoresConCarnetsVencidos()
		{
			EliminarRelacionEntreJugadorYEquipoDeLosQueTenganElCarnetVencido();
			EliminarJugadoresQueNoEstanFichadosEnNingunEquipo();

			return Json("OK", JsonRequestBehavior.AllowGet);
		}

		public JsonResult ListarJugadoresSinFoto()
		{
			var todosLosJugadores = _context.Jugadores;
			var dnisDeJugadoresHuerfanos = todosLosJugadores.Select(x => x.DNI);
			
			var result = new List<string>();
			foreach (var dni in dnisDeJugadoresHuerfanos)
			{
				try
				{
					_imagenesJugadoresPersistence.GetFotoEnBase64(dni);
				}
				catch (Exception e)
				{
					result.Add(dni);
				}
			}

			return Json(result, JsonRequestBehavior.AllowGet);
		}
		
		public JsonResult EliminarJugadoresQueNoEstanFichadosEnNingunEquipo()
		{
			var jugadoresHuerfanos = _context.Jugadores.Where(x => x.JugadorEquipo.Count == 0);
			var dnisDeJugadoresHuerfanos = jugadoresHuerfanos.Select(x => x.DNI).ToList();
			_context.Jugadores.RemoveRange(jugadoresHuerfanos);
			_context.SaveChanges();

			_imagenesJugadoresPersistence.EliminarLista(dnisDeJugadoresHuerfanos);

			return Json("OK", JsonRequestBehavior.AllowGet);
		}

		private void EliminarRelacionEntreJugadorYEquipoDeLosQueTenganElCarnetVencido()
		{
			var jugadoresEquiposConCarnetsVencidos =
				_context.JugadorEquipos
					.Include(x => x.Equipo.Torneo.Tipo)
					.Include(x => x.Jugador)
					.Where(x => DateTime.Now.Year >= x.FechaFichaje.Year + x.Equipo.Torneo.Tipo.ValidezDelCarnetEnAnios);

			_context.JugadorEquipos.RemoveRange(jugadoresEquiposConCarnetsVencidos);
			_context.SaveChanges();
		}
	}
}