using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LigaSoft.Models;
using LigaSoft.Models.Dominio;
using LigaSoft.Models.Otros;
using LigaSoft.Utilidades;
using LigaSoft.ViewModelMappers;
using Microsoft.AspNet.Identity.Owin;

namespace LigaSoft.Controllers
{
	[Authorize(Roles = Roles.Administrador)]
	public class AdministracionDelegadosController : Controller
    {
	    private ApplicationUserManager _userManager;
	    private readonly ApplicationDbContext _context;
	    private readonly UsuarioDelegadoVMM _usuarioDelegadoVMM;

		public AdministracionDelegadosController()
		{			
			_context = new ApplicationDbContext();
			_usuarioDelegadoVMM = new UsuarioDelegadoVMM(_context);
		}

		public ApplicationUserManager UserManager
	    {
		    get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
		    private set => _userManager = value;
	    }

	    protected override void Dispose(bool disposing)
	    {
		    if (disposing)
		    {
			    if (_userManager != null)
			    {
				    _userManager.Dispose();
				    _userManager = null;
			    }
		    }

		    base.Dispose(disposing);
	    }

	    public ActionResult Index()
	    {
		    return View();
	    }

	    public virtual JsonResult GetDelegadosPendientesDeAprobacionForGrid(int? page, int? limit, string sortBy, string direction, string searchField, string searchValue)
	    {
		    var options = new GijgoGridOptions(page, limit, sortBy, direction, searchField, searchValue);

		    var query = _context.UsuariosDelegadosPendientesDeAprobacion.AsQueryable();

		    List<UsuarioDelegado> models;

		    if (!string.IsNullOrWhiteSpace(options.SearchValue))
			    query = query.Where($"{options.SearchField}.Contains(@0)", options.SearchValue);

		    if (!string.IsNullOrEmpty(options.SortBy) && !string.IsNullOrEmpty(options.Direction))
			    query = query.OrderBy(options.Direction.Trim().ToLower() == "asc" ? options.SortBy : $"{options.SortBy} descending");
		    else
			    query = query.OrderBy("Id descending");

		    var total = query.Count();
		    if (options.Page.HasValue && options.Limit.HasValue)
		    {
			    var start = (options.Page.Value - 1) * options.Limit.Value;
			    models = query.Skip(start).Take(options.Limit.Value).ToList();
		    }
		    else
			    models = query.ToList();

		    var records = _usuarioDelegadoVMM.MapForGrid(models);
		    

		    return Json(new { records, total }, JsonRequestBehavior.AllowGet);
	    }

		public async Task<ActionResult> Aprobar(int id)
		{
			var usuarioDelegado = _context.UsuariosDelegadosPendientesDeAprobacion.Single(x => x.Id == id);

			var user = new ApplicationUser { UserName = usuarioDelegado.Email, Email = usuarioDelegado.Email };
			var result = await UserManager.CreateAsync(user, usuarioDelegado.Password);

			if (result.Succeeded)
			{
				await UserManager.AddToRoleAsync(user.Id, Roles.Delegado);
				usuarioDelegado.AspNetUserId = user.Id;
				_context.SaveChanges();
			}

			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
	}
}