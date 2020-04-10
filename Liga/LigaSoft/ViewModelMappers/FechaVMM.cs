using System.Collections.Generic;
using System.Linq;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class FechaVMM : CommonVMM<Fecha, FechaVM>
	{
		public FechaVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreate(FechaVM vm, Fecha model)
		{
			model.DiaDeLaFecha = VMMUtility.ConvertToDateTime(vm.DiaDeLaFecha);
			model.ZonaId = vm.ZonaId;			
			model.Numero = CalcularNumeroDeFecha(vm.ZonaId);

			MapJornadasForCreateAndEdit(vm, model);
		}

		private static void MapJornadasForCreateAndEdit(FechaVM vm, Fecha model)
		{
			model.Jornadas = new List<Jornada>();												

			for (var i = 0; i < vm.CantidadDeJornadas; i++)
			{				
				if (vm.Locales[i] == 0 || vm.Visitantes[i] == 0)
					break;

				var item = new Jornada { LocalId = vm.Locales[i], VisitanteId = vm.Visitantes[i] };

				ModificarJornadaSiEsLibreOInterzonalLocal(vm, i, item);
				ModificarJornadaSiEsLibreOInterzonalVisitante(vm, i, item);

				model.Jornadas.Add(item);
			}
		}

		private static void ModificarJornadaSiEsLibreOInterzonalVisitante(FechaVM vm, int i, Jornada item)
		{
			switch (vm.Visitantes[i])
			{
				case -1:
					item.VisitanteId = null;
					item.QuedoLibre = true;
					break;
				case -2:
					item.VisitanteId = null;
					item.JuegaInterzonal = true;
					break;
			}
		}

		private static void ModificarJornadaSiEsLibreOInterzonalLocal(FechaVM vm, int i, Jornada item)
		{
			switch (vm.Locales[i])
			{
				case -1:
					item.LocalId = null;
					item.QuedoLibre = true;
					break;
				case -2:
					item.LocalId = null;
					item.JuegaInterzonal = true;
					break;
			}
		}

		public void MapForReiniciar(Fecha model)
		{
			model.Publicada = false;
			model.Jornadas = new List<Jornada>();
			for (var i = 0; i < model.Zona.Equipos.Count / 2 + 2; i++)
			{
				var item = new Jornada { LocalId = null, VisitanteId = null };
				model.Jornadas.Add(item);
			}
		}

		public override void MapForEdit(FechaVM vm, Fecha model)
		{
			Context.Partidos.RemoveRange(model.Jornadas.SelectMany(x => x.Partidos));
			Context.Jornadas.RemoveRange(model.Jornadas);
			model.DiaDeLaFecha = VMMUtility.ConvertToDateTime(vm.DiaDeLaFecha);
			model.Jornadas = new List<Jornada>();
			MapJornadasForCreateAndEdit(vm, model);
		}

		public FechaDetailsVM MapForDetailsCustom(Fecha model)
		{
			return new FechaDetailsVM
			{
				DiaDeLaFecha = VMMUtility.ConvertToString(model.DiaDeLaFecha),
				Titulo = model.Descripcion(),
				Jornadas = model.Jornadas.Select(x => x.Descripcion()),
				Publicada = model.Publicada.ToSiNoString()
			};
		}

		private int CalcularNumeroDeFecha(int zonaId)
		{
			var fechaAnterior = Context.Fechas
				.Where(x => x.ZonaId == zonaId)
				.DefaultIfEmpty()
				.Max(x => x == null ? 0 : x.Numero);

			return fechaAnterior + 1;
		}

		public override void MapForCreateAndEdit(FechaVM vm, Fecha model)
		{
			throw new System.NotImplementedException();
		}

		public override IList<FechaVM> MapForGrid(IList<Fecha> modelList)
		{
			var listVM = new List<FechaVM>();

			foreach (var fec in modelList)
				listVM.Add(MapForEditAndDetails(fec));

			return listVM;
		}

		public override FechaVM MapForEditAndDetails(Fecha model)
		{
			return new FechaVM
			{
				Id = model.Id,
				ZonaId = model.ZonaId,
				TorneoId = model.Zona.TorneoId,
				Titulo = model.Descripcion(),
				DiaDeLaFecha = VMMUtility.ConvertToString(model.DiaDeLaFecha),
				Numero = model.Numero
			};
		}
	}
}