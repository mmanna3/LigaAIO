﻿using System.Web.Mvc;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.ViewModels;
using LigaSoft.ViewModelMappers;
using LigaSoft.Utilidades;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class TorneoTipoController : ABMController<TorneoTipo, TorneoTipoVM, TorneoTipoVMM>
    {
	}
}