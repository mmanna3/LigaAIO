﻿using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class JugadorFichadoPorDelegadoVMM : CommonVMM<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM>
	{
		public JugadorFichadoPorDelegadoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(JugadorFichadoPorDelegadoVM vm, JugadorFichadoPorDelegado model)
		{
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.FechaNacimiento = VMMUtility.ConvertToDateTime(vm.FechaNacimiento);
			model.Apellido = vm.Apellido;
		}

		public override JugadorFichadoPorDelegadoVM MapForEditAndDetails(JugadorFichadoPorDelegado model)
		{
			return new JugadorFichadoPorDelegadoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = VMMUtility.ConvertToString(model.FechaNacimiento)
			};
		}
	}
}