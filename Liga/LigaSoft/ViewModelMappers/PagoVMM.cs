using System;
using System.Collections.Generic;
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
	public class PagoVMM : CommonVMM<Pago, PagoVM>
	{
		private readonly ApplicationDbContext _context;

		public PagoVMM(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public override void MapForCreateAndEdit(PagoVM vm, Pago model)
		{
			model.Id = vm.Id;
			model.Fecha = VMMUtility.ConvertToDateTime(vm.Fecha);
			model.Importe = Convert.ToInt32(vm.Importe);
			model.Movimiento = Context.MovimientosEntradaConClub.Find(vm.MovimientoEntradaConClubId);
			model.Comentario = vm.Comentario;
			model.Vigente = true;

			model.FechaAlta = DateTime.Now;
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAlta = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
		}

		public override IList<PagoVM> MapForGrid(IList<Pago> modelList)
		{
			var listVM = new List<PagoVM>();

			foreach (var cat in modelList)
				listVM.Add(MapForEditAndDetails(cat));

			return listVM;
		}

		public override PagoVM MapForEditAndDetails(Pago model)
		{			
			return new PagoVM
			{
				Id = model.Id,
				Fecha = VMMUtility.ConvertToString(model.Fecha),
				Importe = model.Importe.ToString(),
				MovimientoEntradaConClubId = model.MovimientoEntradaConClubId,
				Comentario = model.Comentario,
				TotalDelMovimiento = $"${model.Movimiento.Total}",
				SaldoDeudor = $"${model.Movimiento.ImporteAdeudado()}",				
				Alta = $"{model.UsuarioAlta.Email} - {model.FechaAlta}",
				Anulacion = $"{model.UsuarioAnulacion?.Email} - {model.FechaAnulacion}",
				Vigente = model.Vigente.ToSiNoString()
			};
		}

		public void MapAnular(Pago model)
		{
			model.Vigente = false;
			model.FechaAnulacion = DateTime.Now;
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAnulacion = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
		}
	}
}