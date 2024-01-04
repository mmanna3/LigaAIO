using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class InformeVMM
	{
		private readonly ApplicationDbContext _context;

		public InformeVMM(ApplicationDbContext context)
		{
			_context = context;
		}

		public InformeVM Map(RangoVM rango)
		{
			var fecIni = DateTimeUtils.ConvertToDateTime(rango.FechaInicio);
			var fecFin = DateTimeUtils.ConvertToDateTime(rango.FechaFin);

			var pagosEnEfectivo = _context.Pagos.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Efectivo);
			var pagosVirtuales = _context.Pagos.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Virtual);

			var vm = new InformeVM();

			vm.FechaInicio = rango.FechaInicio;
			vm.FechaFin = rango.FechaFin;
			vm.Insumos.Efectivo = $"{pagosEnEfectivo.Where(x => x.Movimiento.Concepto.Id >= 4).ToList().Sum(x => x.Importe)}";
			vm.Libres.Efectivo = $"{pagosEnEfectivo.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Libre).ToList().Sum(x => x.Importe)}";
			vm.Cuotas.Efectivo = $"{pagosEnEfectivo.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Cuota).ToList().Sum(x => x.Importe)}";
			vm.Fichajes.Efectivo = $"{pagosEnEfectivo.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Fichaje).ToList().Sum(x => x.Importe)}";
			
			vm.Insumos.Virtual = $"{pagosVirtuales.Where(x => x.Movimiento.Concepto.Id >= 4).ToList().Sum(x => x.Importe)}";
			vm.Libres.Virtual = $"{pagosVirtuales.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Libre).ToList().Sum(x => x.Importe)}";
			vm.Cuotas.Virtual = $"{pagosVirtuales.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Cuota).ToList().Sum(x => x.Importe)}";
			vm.Fichajes.Virtual = $"{pagosVirtuales.Where(x => x.Movimiento.Concepto.Id == (int) ConceptoTipoEnum.Fichaje).ToList().Sum(x => x.Importe)}";

			vm.CajaEdefiIngresos.Efectivo = $"{_context.MovimientosEntradaSinClub.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Efectivo).ToList().Sum(y => y.Total)}";
			vm.CajaEdefiIngresos.Virtual = $"{_context.MovimientosEntradaSinClub.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Virtual).ToList().Sum(y => y.Total)}";
			
			vm.CajaEdefiEgresos.Efectivo =$"{_context.MovimientosSalida.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Efectivo).ToList().Sum(y => y.Total)}";
			vm.CajaEdefiEgresos.Virtual =$"{_context.MovimientosSalida.Where(x => x.Fecha >= fecIni && x.Fecha <= fecFin && x.Vigente && x.FormaDePago == FormaDePago.Virtual).ToList().Sum(y => y.Total)}";

			vm.CalcularTotales();
			vm.Formatear();

			return vm;
		}

		public InformeCantidadDeJugadoresPorTorneoVM CantidadDeJugadoresPorTorneoMap(int? anio = null)
		{
			if (anio == null)
				anio = DateTime.Now.Year;

			var vm = new InformeCantidadDeJugadoresPorTorneoVM
			{
				Renglones = _context.JugadorEquipos
									.Where(x => (int)x.Equipo.Torneo.Anio == anio)
									.GroupBy(x => x.Equipo.Torneo.Tipo)
									.Select(x => new InformeJugadoresPorTorneoRenglonVM
									{
										TorneoTipo = x.FirstOrDefault().Equipo.Torneo.Tipo.Descripcion,
										CantidadDeJugadores = x.Count(y => y.JugadorId != 0)
									})
									.ToList()
			};

			return vm;
		}

		public InformeMovimientosImpagosVM MovimientosImpagosMap()
		{
			var movClubs = _context.MovimientosEntradaConClub.Where(x => x.Vigente).ToList().Where(x => x.ImporteAdeudado() > 0).ToList();

			var vm = new InformeMovimientosImpagosVM();

			vm.ClubesDeudores = movClubs.Select(x => $"<a href='/Club/{x.ClubId}/MovimientoEntradaConClub/Index/'>{x.Club.Nombre}</a>").Distinct().OrderBy(x => x).ToList();

			vm.Insumos = $"{movClubs.Where(x => x.Concepto.Id >= 4).Sum(x => x.ImporteAdeudado())}";
			vm.Libres = $"{movClubs.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Libre).Sum(x => x.ImporteAdeudado())}";
			vm.Cuotas = $"{movClubs.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Cuota).Sum(x => x.ImporteAdeudado())}";
			vm.Fichajes = $"{movClubs.Where(x => x.Concepto.Id == (int)ConceptoTipoEnum.Fichaje).Sum(x => x.ImporteAdeudado())}";

			vm.Total = $"{vm.CalcularTotal()}";
			vm.Formatear();

			return vm;
		}

		public InformePagoCuotasPorMesVM PagoCuotasPorMesMap()
		{
			var vm = new InformePagoCuotasPorMesVM();

			vm.Renglones = _context.MovimientosEntradaConClubCuota
				.Where(x => x.Vigente && x.Fecha.Year == DateTime.Now.Year)
				.ToList()
				.GroupBy(x => x.ClubId)
				.Select(r => new ClubDeudaCuotaPorMesRenglonVM
				{
					Id = r.First().ClubId,
					ClubNombre = r.First().Club.Nombre,
					ClubLink = $"<a href='/Club/{r.First().ClubId}/MovimientoEntradaConClub/Index/'>{r.First().Club.Nombre}</a>",
					ValorCuota = $"${r.First().Club.Cuota()}",
					PagoAbril = $"${r.Where(x => x.Mes == Mes.Abril).Sum(c => c.ImportePagado())}",
					PagoMayo = $"${r.Where(x => x.Mes == Mes.Mayo).Sum(c => c.ImportePagado())}",
					PagoJunio = $"${r.Where(x => x.Mes == Mes.Junio).Sum(c => c.ImportePagado())}",
					PagoJulio = $"${r.Where(x => x.Mes == Mes.Julio).Sum(c => c.ImportePagado())}",
					PagoAgosto = $"${r.Where(x => x.Mes == Mes.Agosto).Sum(c => c.ImportePagado())}",
					PagoSeptiembre = $"${r.Where(x => x.Mes == Mes.Septiembre).Sum(c => c.ImportePagado())}",
					PagoOctubre = $"${r.Where(x => x.Mes == Mes.Octubre).Sum(c => c.ImportePagado())}",
					PagoNoviembre = $"${r.Where(x => x.Mes == Mes.Noviembre).Sum(c => c.ImportePagado())}"
				})
				.ToList();

			AgregarClubesSinMovimientos(vm);

			vm.OrdenarAlfabeticamentePorNombreDeClub();

			return vm;
		}

		private void AgregarClubesSinMovimientos(InformePagoCuotasPorMesVM vm)
		{
			foreach (var club in _context.Clubs.ToList())
			{
				if (!vm.Renglones.Select(x => x.Id).Contains(club.Id))
					vm.Renglones.Add(new ClubDeudaCuotaPorMesRenglonVM
					{
						Id = club.Id,
						ClubLink = $"<a href='/Club/{club.Id}/MovimientoEntradaConClub/Index/'>{club.Nombre}</a>",
						ClubNombre = club.Nombre,
						ValorCuota = $"${club.Cuota()}",
						PagoAbril = "$0",
						PagoMayo = "$0",
						PagoJunio = "$0",
						PagoJulio = "$0",
						PagoAgosto = "$0",
						PagoSeptiembre = "$0",
						PagoOctubre = "$0",
						PagoNoviembre = "$0"
					});
			}
		}
	}
}