using System.Collections.Generic;
using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class TorneoVMM : CommonVMM<Torneo, TorneoVM>
	{
		public TorneoVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(TorneoVM vm, Torneo model)
		{
			model.Id = vm.Id;
			model.Anio = vm.Anio;
			model.TipoId = vm.TipoId;
		}

		public override IList<TorneoVM> MapForGrid(IList<Torneo> torneos)
		{
			var listVM = new List<TorneoVM>();

			foreach (var torneo in torneos.OrderByDescending(x => x.Anio).ThenByDescending(x => x.Publico))
				listVM.Add(MapForEditAndDetails(torneo));

			return listVM;
		}

		public override TorneoVM MapForEditAndDetails(Torneo model)
		{
			return new TorneoVM
			{
				Id = model.Id,
				Anio = model.Anio,
				TipoId = model.TipoId,
				TipoDesc = model.Tipo.Descripcion,
				Formato = model.Tipo.Formato.Descripcion(),
				VisibleEnWebPublica = model.Publico.ToSiNoString(),
				SancionesHabilitadas = model.SancionesHabilitadas.ToSiNoString(),
				Zonas = string.Join(", ", model.Zonas.Select(x => x.Nombre)),
				Categorias = string.Join(", ", model.Categorias.Select(x => x.Nombre))
			};
		}

		public void MapFinalizar(Torneo model)
		{
			foreach (var equipo in model.Zonas.SelectMany(x => x.Equipos))
			{
				equipo.Zona = null;
				equipo.Torneo = null;				
			}

			model.Publico = false;
		}
	}
}