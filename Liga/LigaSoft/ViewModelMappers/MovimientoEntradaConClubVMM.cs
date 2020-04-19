using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio.Finanzas;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LigaSoft.ViewModelMappers
{
	public class MovimientoEntradaConClubVMM : CommonVMM<MovimientoEntradaConClub, MovimientoEntradaConClubVM>
	{
		private readonly ApplicationDbContext _context;
		private readonly PagoVMM _pagoVMM;

		public MovimientoEntradaConClubVMM(ApplicationDbContext context) : base(context)
		{
			_context = context;
			_pagoVMM = new PagoVMM(context);
		}

		public override void MapForCreateAndEdit(MovimientoEntradaConClubVM conClubVM, MovimientoEntradaConClub model)
		{
			model.Id = conClubVM.Id;
			model.Fecha = DateTimeUtils.ConvertToDateTime(conClubVM.Fecha);
			model.Club = Context.Clubs.Find(conClubVM.ClubId);
			model.Concepto = Context.Conceptos.Find(conClubVM.ConceptoId);
			model.Cantidad = conClubVM.Cantidad;
			model.Comentario = conClubVM.Comentario;
			model.PrecioUnitario = Convert.ToInt32(conClubVM.PrecioUnitario);
			model.Vigente = true;
			model.Total = model.Cantidad * model.PrecioUnitario;

			model.FechaAlta = DateTime.Now;
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAlta = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
		}

		public override IList<MovimientoEntradaConClubVM> MapForGrid(IList<MovimientoEntradaConClub> modelList)
		{
			var listVM = new List<MovimientoEntradaConClubVM>();

			foreach (var cat in modelList)
				listVM.Add(MapForEditAndDetails(cat));

			return listVM;
		}

		public override MovimientoEntradaConClubVM MapForEditAndDetails(MovimientoEntradaConClub model)
		{			
			var result = new MovimientoEntradaConClubVM
			{
				Id = model.Id,
				Fecha = DateTimeUtils.ConvertToString(model.Fecha),
				Club = model.Club?.Nombre,
				ClubId = (int)model.ClubId,
				Concepto = model.Concepto.Descripcion,
				Comentario = model.Comentario,
				Alta = $"{model.UsuarioAlta.Email} - {model.FechaAlta}",
				Anulacion = $"{model.UsuarioAnulacion?.Email} - {model.FechaAnulacion}",
				PrecioUnitario = $"${model.PrecioUnitario}",
				Cantidad = model.Cantidad,
				Vigente = model.Vigente.ToSiNoString(),
				Tipo = model.Concepto.Tipo(),
				Total = $"${model.Total}",
				Pagado = $"${model.ImportePagado()}",
				Deuda = $"${model.ImporteAdeudado()}"
			};

			if (model.GetType().BaseType == typeof(MovimientoEntradaConClubCuota))
				result.Concepto = $"Cuota {((MovimientoEntradaConClubCuota) model).Mes.Descripcion()}";

			return result;
		}

		public void MapAnular(MovimientoEntradaConClub model)
		{
			model.Vigente = false;
			model.FechaAnulacion = DateTime.Now;

			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAnulacion = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());

			if (model.Concepto is ConceptoInsumo)
			{
				var conceptoInsumo = Context.ConceptosInsumo.Single(x => x.Id == model.ConceptoId);
				conceptoInsumo.Stock += model.Cantidad;
			}

			foreach (var pago in model.Pagos)
				_pagoVMM.MapAnular(pago);
		}

		public void MapForCreateMovimientoCuota(MovimientoEntradaConClubCuotaVM vm, MovimientoEntradaConClubCuota model)
		{
			MapForCreateAndEdit(vm, model);
			model.Mes = vm.Mes;
		}
	}
}