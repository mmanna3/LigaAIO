using System;
using System.Collections.Generic;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

namespace LigaSoft.ViewModelMappers
{
	public class JugadorFichadoPorDelegadoVMM : CommonVMM<JugadorFichadoPorDelegado, JugadorFichadoPorDelegadoVM>
	{
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public JugadorFichadoPorDelegadoVMM(ApplicationDbContext context) : base(context)
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}

		public override void MapForCreateAndEdit(JugadorFichadoPorDelegadoVM vm, JugadorFichadoPorDelegado model)
		{
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.FechaNacimiento = DateTimeUtils.ConvertToDateTime(vm.FechaNacimiento);
			model.Apellido = vm.Apellido;
			model.EquipoId = vm.EquipoId;
			model.Estado = EstadoJugadorFichadoPorDelegado.PendienteDeAprobacion;
			model.MotivoDeRechazo = null;
		}

		public override JugadorFichadoPorDelegadoVM MapForEditAndDetails(JugadorFichadoPorDelegado model)
		{
			var equipo = Context.Equipos.Find(model.EquipoId);
			return new JugadorFichadoPorDelegadoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				Equipo = equipo.Nombre,
				Club = equipo.Club.Nombre,
				EquipoId = model.EquipoId,
				Estado = model.Estado,
				FotoCarnetRelativePath = _imagenesJugadoresDiskPersistence.PathFotoTemporalCarnet(model.DNI),
				FotoDNIFrenteRelativePath = _imagenesJugadoresDiskPersistence.PathFotoTemporalDNIFrente(model.DNI),
				MotivoDeRechazo = model.MotivoDeRechazo
		};
		}
	}
}