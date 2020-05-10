using System;
using System.Collections.Generic;
using LigaSoft.ExtensionMethods;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;

namespace LigaSoft.ViewModelMappers
{
	public class NoticiaVMM : CommonVMM<Noticia, NoticiaVM>
	{
		public NoticiaVMM(ApplicationDbContext context) : base(context)
		{
		}

		public override void MapForCreateAndEdit(NoticiaVM vm, Noticia model)
		{
			model.Id = vm.Id;
			model.Fecha = DateTime.Now;
			model.Titulo = vm.Titulo;
			model.Cuerpo = vm.Cuerpo;
			model.Subtitulo = vm.Subtitulo;
			model.Visible = true;
		}

		public override void MapForEdit(NoticiaVM vm, Noticia model)
		{
			model.Titulo = vm.Titulo;
			model.Subtitulo = vm.Subtitulo;
			model.Cuerpo = vm.Cuerpo;
		}

		public override IList<NoticiaVM> MapForGrid(IList<Noticia> modelList)
		{
			var listVM = new List<NoticiaVM>();

			foreach (var cat in modelList)
				listVM.Add(MapForEditAndDetails(cat));

			return listVM;
		}

		public override NoticiaVM MapForEditAndDetails(Noticia model)
		{
			return new NoticiaVM
			{
				Id = model.Id,
				Fecha = DateTimeUtils.ConvertToString(model.Fecha),
				Titulo = model.Titulo,
				Subtitulo = model.Subtitulo,
				Cuerpo = model.Cuerpo,				
				Visible = model.Visible.ToSiNoString()
			};
		}
	}
}