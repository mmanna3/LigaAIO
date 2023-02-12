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

		public GeneradorDeMovimientos(ApplicationDbContext context)
		{
		}

		public MovimientoEntradaConClub GenerarMovimientoFichajeYSuPago(Equipo equipo, string dni)
		{
			var movimiento = GenerarMovimientoFichaje(equipo, dni);
			var pago = GenerarPagoDelMovimientoFichaje(dni, movimiento);
			movimiento.Pagos = new List<Pago>{pago};

			return movimiento;
		}

		public MovimientoEntradaConClub GenerarMovimientoFichajeImpago(Equipo equipo, string dni)
		{
			return GenerarMovimientoFichaje(equipo, dni);
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

		private MovimientoEntradaConClub GenerarMovimientoFichaje(Equipo equipo, string dni)
		{			
			var movimiento = new MovimientoEntradaConClub
			{
				ConceptoId = (int)ConceptoTipoEnum.Fichaje,
				ClubId = equipo.ClubId,
				Cantidad = 1,
				Comentario = $"Movimiento generado automáticamente al fichar al jugador con DNI:{dni}",
				Fecha = DateTime.Now,
				FechaAlta = DateTime.Now,
				PrecioUnitario = equipo.Torneo.Tipo.ValorDelFichajeEnPesos,
				Total = equipo.Torneo.Tipo.ValorDelFichajeEnPesos,
				Vigente = true,
				UsuarioAltaId = HttpContext.Current.User.Identity.GetUserId()
			};			

			return movimiento;
		}
	}
}