using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Enums;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.Utilidades.Persistence;
using LigaSoft.Utilidades.Persistence.DiskPersistence;

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
			model.Id = vm.Id;
			model.DNI = vm.DNI;
			model.Nombre = vm.Nombre;
			model.FechaNacimiento = DateTimeUtils.ConvertToDateTime(vm.FechaNacimiento);
			model.Apellido = vm.Apellido;
			model.EquipoId = vm.CodigoEquipo;
			model.Estado = EstadoJugadorAutofichado.PendienteDeAprobacion;
			model.MotivoDeRechazo = null;
		}

		public override JugadorAutofichadoVM MapForEditAndDetails(JugadorAutofichado model)
		{
			var equipo = Context.Equipos.Find(model.EquipoId);
			return new JugadorAutofichadoVM
			{
				Id = model.Id,
				Nombre = model.Nombre,
				Apellido = model.Apellido,
				DNI = model.DNI,
				FechaNacimiento = DateTimeUtils.ConvertToString(model.FechaNacimiento),
				Equipo = equipo.Nombre,
				Club = equipo.Club.Nombre,
				CodigoEquipo = model.EquipoId,
				Estado = model.Estado,
				EstadoDescripcion = model.Estado.Descripcion(),
				FotoCarnetRelativePath = _imagenesJugadoresDiskPersistence.PathFotoTemporalCarnet(model.DNI),
				FotoDNIFrenteRelativePath = _imagenesJugadoresDiskPersistence.PathFotoTemporalDNIFrente(model.DNI),
				FotoDNIDorsoRelativePath = _imagenesJugadoresDiskPersistence.PathFotoTemporalDNIDorso(model.DNI),
				MotivoDeRechazo = model.MotivoDeRechazo
		};
		}
	}
}