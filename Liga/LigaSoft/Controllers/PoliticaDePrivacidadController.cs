using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using LigaSoft.BusinessLogic;
using LigaSoft.Models.ViewModels;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Newtonsoft.Json;
using LigaSoft.Models;

namespace LigaSoft.Controllers
{
	[AllowAnonymous]
	public class PoliticaDePrivacidadController : Controller
    {

		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}
	}
}