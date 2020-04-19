using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class SancionVMM : CommonVMM<Sancion, SancionVM>
	{
		public SancionVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(SancionVM vm, Sancion model)
		{
			model.Id = vm.Id;
			model.JornadaId = vm.JornadaId;
			model.CategoriaId = vm.CategoriaId;
			model.Descripcion = vm.Descripcion;
			model.Dia = DateTimeUtils.ConvertToDateTime(vm.Dia);
			model.CantidadFechasQueAdeuda = vm.CantidadFechasQueAdeuda;
			model.Visible = true;
		}

		public override SancionVM MapForEditAndDetails(Sancion model)
		{
			return new SancionVM
			{
				Id = model.Id,
				JornadaId = model.JornadaId,
				Descripcion = model.Descripcion,
				CategoriaId = model.CategoriaId,
				ZonaId = model.Jornada.Fecha.ZonaId,
				TorneoId = model.Jornada.Fecha.Zona.TorneoId,
				Dia = DateTimeUtils.ConvertToString(model.Dia),
				Fecha = model.Jornada.Fecha.Numero.ToString(),
				Local = model.Jornada.NombreDelLocal(),
				Visitante = model.Jornada.NombreDelVisitante(),
				Categoria = model.Categoria.Nombre,
				CantidadFechasQueAdeuda = model.CantidadFechasQueAdeuda,
				Visible = model.Visible.ToSiNoString()
			};
		}

		public override SancionVM MapForEdit(Sancion model)
		{
			var vm = InitSancionVM(model.Jornada.Fecha.ZonaId);

			vm.FechaId = model.Jornada.FechaId;
			vm.JornadaId = model.JornadaId;
			vm.CantidadFechasQueAdeuda = model.CantidadFechasQueAdeuda;
			vm.CategoriaId = model.CategoriaId;
			vm.Descripcion = model.Descripcion;
			vm.Dia = model.Dia.ToString("dd-MM-yyyy");

			return vm;
		}

		public SancionVM InitSancionVM(int zonaId)
		{
			var fechasDeLaZona = Context.Fechas.Where(x => x.ZonaId == zonaId)
				.ToList()
				.Select(x => new TextValueItem { Text = $"Fecha {x.Numero}", Value = x.Id.ToString() })
				.ToList();

			var categoriasDelTorneo = Context.Zonas.SingleOrDefault(x => x.Id == zonaId).Torneo.Categorias
				.Select(x => new TextValueItem { Text = $"{x.Nombre}", Value = x.Id.ToString() })
				.ToList();

			var vm = new SancionVM
			{
				ZonaId = zonaId,
				FechasDeLaZona = fechasDeLaZona,
				CategoriasDelTorneo = categoriasDelTorneo
			};

			return vm;
		}
	}
}