using LigaSoft.BusinessLogic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;
using System;

namespace LigaSoft.ViewModelMappers
{
	public class JugadorAutofichadoVMM : CommonVMM<JugadorAutofichado, JugadorAutofichadoVM>
	{
		private readonly IImagenesJugadoresPersistence _imagenesJugadoresDiskPersistence;

		public JugadorAutofichadoVMM(ApplicationDbContext context) : base(context)
		{
			_imagenesJugadoresDiskPersistence = new ImagenesJugadoresDiskPersistence(new AppPathsWebApp());
		}

		public override void MapForCreateAndEdit(JugadorAutofichadoVM vm, JugadorAutofichado model)
		{
			int equipoId;
			try
			{
				equipoId = GeneradorDeHash.ObtenerSemillaAPartirDeAlfanumerico7Digitos(vm.CodigoAlfanumerico);
			}
			catch (Exception e)
			{
				throw e;
			}

			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.FechaNacimiento = DateTimeUtils.ConvertToDateTime(vm.FechaNacimiento);
			model.Apellido = vm.Apellido;
			model.EquipoId = equipoId;
			model.Estado = EstadoJugadorAutofichado.PendienteDeAprobacion;
			model.MotivoDeRechazo = null;
		}

		public override JugadorAutofichadoVM MapForEditAndDetails(JugadorAutofichado model)
		{
			var equipo = Context.Equipos.Find(model.EquipoId);
			var fechaAntiCache = DateTime.Now.ToString("dd-MM-yy--HH-mm-ss-ff");

			return new JugadorAutofichadoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				Equipo = equipo.Nombre,
				EquipoId = model.EquipoId,
				CodigoAlfanumerico = GeneradorDeHash.GenerarAlfanumerico7Digitos(model.EquipoId),
				Club = equipo.Club.Nombre,				
				Estado = model.Estado,
				EstadoDescripcion = model.Estado.Descripcion(),
				FotoCarnetRelativePath = $"{_imagenesJugadoresDiskPersistence.PathFotoTemporalCarnet(model.DNI)}?{fechaAntiCache}",
				FotoDNIFrenteRelativePath = $"{_imagenesJugadoresDiskPersistence.PathFotoTemporalDNIFrente(model.DNI)}?{fechaAntiCache}",
				FotoDNIDorsoRelativePath = $"{_imagenesJugadoresDiskPersistence.PathFotoTemporalDNIDorso(model.DNI)}?{fechaAntiCache}",
				MotivoDeRechazo = model.MotivoDeRechazo
		};
		}

		public JugadorAutofichadoBaseVM MapForBaseDetails(JugadorAutofichado model)
		{
			return new JugadorAutofichadoBaseVM
			{
				Id = model.Id,
				Equipo = model.Equipo.Nombre,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				Estado = model.Estado,
				EstadoDescripcion = model.Estado.Descripcion(),
				MotivoDeRechazo = model.MotivoDeRechazo
			};
		}
	}
}