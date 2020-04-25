using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.Enums;
using Microsoft.AspNet.Identity;

namespace LigaSoft.BusinessLogic
{
	public class GeneradorDeMovimientos
	{
		private readonly int _valorPorDefectoEnPesosDelConceptoFichaje;

		public GeneradorDeMovimientos(ApplicationDbContext context)
		{
			_valorPorDefectoEnPesosDelConceptoFichaje = context.ParametrizacionesGlobales.Select(x => x.ValorPorDefectoEnPesosDelConceptoFichaje).First();
		}

		public MovimientoEntradaConClub GenerarMovimientoFichajeYSuPago(Club club, string dni)
		{
			var movimiento = GenerarMovimientoFichaje(club, dni);
			var pago = GenerarPagoDelMovimientoFichaje(dni, movimiento);
			movimiento.Pagos = new List<Pago>{pago};

			return movimiento;
		}

		public MovimientoEntradaConClub GenerarMovimientoFichajeImpago(Club club, string dni)
		{
			return GenerarMovimientoFichaje(club, dni);
		}

		private static Pago GenerarPagoDelMovimientoFichaje(string dni, MovimientoEntradaConClub movimiento)
		{
			return new Pago
			{
				Movimiento = movimiento,
				Comentario = $"Pago generado automáticamente al fichar al jugador con DNI:{dni}",
				Fecha = DateTime.Now,
				FechaAlta = DateTime.Now,
				Vigente = true,
				UsuarioAltaId = HttpContext.Current.User.Identity.GetUserId(),
				Importe = movimiento.Total
			};
		}

		private MovimientoEntradaConClub GenerarMovimientoFichaje(Club club, string dni)
		{			
			var movimiento = new MovimientoEntradaConClub
			{
				ConceptoId = (int)ConceptoTipoEnum.Fichaje,
				ClubId = club.Id,
				Cantidad = 1,
				Comentario = $"Movimiento generado automáticamente al fichar al jugador con DNI:{dni}",
				Fecha = DateTime.Now,
				FechaAlta = DateTime.Now,
				PrecioUnitario = _valorPorDefectoEnPesosDelConceptoFichaje,
				Total = _valorPorDefectoEnPesosDelConceptoFichaje,
				Vigente = true,
				UsuarioAltaId = HttpContext.Current.User.Identity.GetUserId()
			};			

			return movimiento;
		}
	}
}