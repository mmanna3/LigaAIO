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
	public class MovimientoEntradaSinClubVMM : CommonVMM<MovimientoEntradaSinClub, MovimientoEntradaSinClubVM>
	{
		private readonly ApplicationDbContext _context;

		public MovimientoEntradaSinClubVMM(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public override void MapForCreateAndEdit(MovimientoEntradaSinClubVM vm, MovimientoEntradaSinClub model)
		{
			model.Id = vm.Id;
			model.Fecha = VMMUtility.ConvertToDateTime(vm.Fecha);
			model.Comentario = vm.Comentario;
			model.Vigente = true;
			model.Total = Convert.ToInt32(vm.Total);

			model.FechaAlta = DateTime.Now;
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAlta = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
		}

		public override IList<MovimientoEntradaSinClubVM> MapForGrid(IList<MovimientoEntradaSinClub> modelList)
		{
			var listVM = new List<MovimientoEntradaSinClubVM>();

			foreach (var cat in modelList)
				listVM.Add(MapForEditAndDetails(cat));

			return listVM;
		}

		public override MovimientoEntradaSinClubVM MapForEditAndDetails(MovimientoEntradaSinClub model)
		{			
			return new MovimientoEntradaSinClubVM
			{
				Id = model.Id,
				Fecha = VMMUtility.ConvertToString(model.Fecha),
				Comentario = model.Comentario,
				Alta = $"{model.UsuarioAlta.Email} - {model.FechaAlta}",
				Anulacion = $"{model.UsuarioAnulacion?.Email} - {model.FechaAnulacion}",
				Vigente = model.Vigente.ToSiNoString(),
				Total = $"${model.Total}"
			};
		}

		public void MapAnular(MovimientoEntradaSinClub model)
		{
			model.Vigente = false;
			model.FechaAnulacion = DateTime.Now;
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
			model.UsuarioAnulacion = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
		}
	}
}