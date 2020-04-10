using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;

namespace LigaSoft.ViewModelMappers
{
	public class JornadaVMM : CommonVMM<Jornada, JornadaVM>
	{
		public JornadaVMM(ApplicationDbContext context) : base(context)
		{
		}

		public void MapForCargarPartidos(JornadaVM vm, Jornada model)
		{
			var jornada = Context.Jornadas.FirstOrDefault(x => x.Id == vm.Id);
			var partidoVMM = new PartidoVMM(Context);

			if (!jornada.Partidos.Any())	//Primera vez que se cargan resultados
			{
				foreach (var partidoVM in vm.Partidos)
				{
					var partidoModel = new Partido();
					partidoVMM.MapForCreateAndEdit(partidoVM, partidoModel);
					model.Partidos.Add(partidoModel);
				}
			}
			else	//Edición
			{
				foreach (var partidoModel in jornada.Partidos)
				{
					var partidoVM = vm.Partidos.Single(x => x.CategoriaId == partidoModel.CategoriaId);
					partidoVMM.MapForCreateAndEdit(partidoVM, partidoModel);
				}
			}
		}

		public JornadaVM MapForCargarPartidos(int id)
		{
			var jornada = Context.Jornadas.Single(x => x.Id == id);			

			var vm = new JornadaVM
			{
				Id = id,
				FechaId = jornada.FechaId,
				Titulo = $"{jornada.Descripcion()}",
				Subtitulo = $"{jornada.Fecha.Descripcion()}",
				Partidos = new List<PartidoVM>()
			};

			foreach (var categoria in jornada.Fecha.Zona.Torneo.Categorias)
			{
				var partido = Context.Partidos.FirstOrDefault(x => x.JornadaId == id && x.CategoriaId == categoria.Id);

				vm.Partidos.Add(new PartidoVM
				{
					CategoriaId = categoria.Id,
					Categoria = categoria.Nombre,
					Orden = categoria.Orden,
					GolesLocal = partido != null ? partido.GolesLocal : "",
					GolesVisitante = partido != null ? partido.GolesVisitante : ""
				});
			}

			vm.Partidos.Sort((x, y) => x.Orden.CompareTo(y.Orden));

			return vm;
		}

		internal void MapForCargarGoleadores(JornadaVM vm, Jornada model)
		{
			throw new NotImplementedException();
		}

		public override void MapForCreateAndEdit(JornadaVM vm, Jornada model)
		{
			throw new System.NotImplementedException();
		}

		public override IList<JornadaVM> MapForGrid(IList<Jornada> list)
		{
			var listVM = new List<JornadaVM>();

			foreach (var item in list)
				listVM.Add(MapForGrilla(item));

			return listVM;
		}

		private static JornadaVM MapForGrilla(Jornada model)
		{
			return new JornadaVM
			{
				Id = model.Id,
				Local = model.NombreDelLocal(),
				Visitante = model.NombreDelVisitante(),
				ResultadosVerificados = model.ResultadosVerificados.ToSiNoString()
			};
		}

		public override JornadaVM MapForEditAndDetails(Jornada model)
		{
			var partidoVMM = new PartidoVMM(Context);

			return new JornadaVM
			{
				Id = model.Id,
				LocalId = model.LocalIdInt(),
				VisitanteId = model.VisitanteIdInt(),
				FechaId = model.FechaId,
				Titulo = $"{model.Descripcion()}",
				Subtitulo = $"{model.Fecha.Descripcion()}",
				Partidos = (List<PartidoVM>) partidoVMM.MapForGrid(model.Partidos.ToList()),
				ResultadosVerificadosBool = model.ResultadosVerificados,
				ResultadosVerificados = model.ResultadosVerificados.ToSiNoString()
			};
		}

		public void MapForVerificarResultados(JornadaVM vm, Jornada model)
		{
			model.ResultadosVerificados = vm.ResultadosVerificadosBool;
		}

		public object MapForCargarGoleadores(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}