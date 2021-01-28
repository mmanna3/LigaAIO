using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Security.Cryptography.X509Certificates;
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

		private void EliminarJugadoresQueNoEstanFichadosEnNingunEquipo()
		{
			var jugadoresHuerfanos = _context.Jugadores.Where(x => x.JugadorEquipo.Count == 0);
			var dnisDeJugadoresHuerfanos = jugadoresHuerfanos.Select(x => x.DNI).ToList();
			_context.Jugadores.RemoveRange(jugadoresHuerfanos);
			_context.SaveChanges();

			_imagenesJugadoresPersistence.EliminarLista(dnisDeJugadoresHuerfanos);
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