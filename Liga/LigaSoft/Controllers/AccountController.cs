using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LigaSoft.Models;
using LigaSoft.Models.Attributes.GPRPattern;
using LigaSoft.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace LigaSoft.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(vm.Usuario, vm.Password, vm.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = vm.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Usuario o contraseña incorrecto.");
                    return View(vm);
            }
        }

        [HttpPost]
        [AllowAnonymous]      
        public async Task<JsonResult> LoginDeAppDelegados(LoginAppDelegadosViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new LoginAppDelegadosRespuestaViewModel(false, "Todos los campos son obligatorios"), JsonRequestBehavior.AllowGet);

            var result = await SignInManager.PasswordSignInAsync(model.Usuario, model.Password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                {
                    var context = new ApplicationDbContext();
                    var aspNetUser = context.Users.Single(x => x.UserName == model.Usuario);

                    var usuarioDelegado = context.UsuariosDelegados.Include(usuarioDelegado1 => usuarioDelegado1.Club).SingleOrDefault(x => x.Usuario == aspNetUser.UserName);

                    var resultado = new LoginAppDelegadosRespuestaViewModel(true);
                    resultado.Usuario = aspNetUser.UserName;
                    resultado.Club = usuarioDelegado != null ? usuarioDelegado.Club?.Nombre : "";
                    resultado.ClubId = usuarioDelegado?.ClubId ?? -1;
                    
                    return Json(resultado, JsonRequestBehavior.AllowGet);
                }
                case SignInStatus.Failure:
                default:
                    return Json(new LoginAppDelegadosRespuestaViewModel(false, "Usuario o contraseña incorrecto."), JsonRequestBehavior.AllowGet);
            }
        }

        [ImportModelStateFromTempData]
		public ActionResult CambiarPassword()
	    {
		    return View();
	    }

	    [ExportModelStateToTempData, HttpPost]
		public async Task<ActionResult> CambiarPassword(CambiarPasswordVM vm)
	    {
		    if (!ModelState.IsValid)
			    return RedirectToAction("CambiarPassword");

			var context = new ApplicationDbContext();

		    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
		    var user = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

		    var verificacionPasswordAnterior = userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, vm.PasswordAnterior);

		    if (verificacionPasswordAnterior != PasswordVerificationResult.Success)
		    {
			    ModelState.AddModelError("", "El password ingresado no es el correcto.");
			    return RedirectToAction("CambiarPassword");
		    }

			user.PasswordHash = userManager.PasswordHasher.HashPassword(vm.PasswordNuevo);
		    var result = await userManager.UpdateAsync(user);
		    if (!result.Succeeded)
		    {
				ModelState.AddModelError("", "El password no se pudo cambiar.");
				return RedirectToAction("CambiarPassword");
			}

			return RedirectToAction("Index", "Torneo");
	    }

        [AllowAnonymous, HttpPost]
        public async Task<JsonResult> CambiarPasswordAppDelegados(CambiarPasswordAppDelegadosVM vm)
        {
            var context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = userManager.Users.SingleOrDefault(x => x.UserName == vm.Usuario);
            
            var usuarioDelegado = context.UsuariosDelegados.SingleOrDefault(x => x.AspNetUserId == user.Id);
            
            if (user ==  null || usuarioDelegado == null)
                return Json(JsonConvert.SerializeObject(ApiResponseCreator.Error("El usuario no existe")), JsonRequestBehavior.AllowGet); 

            if (usuarioDelegado.BlanqueoDeClavePendiente == false)
                return Json(JsonConvert.SerializeObject(ApiResponseCreator.Error("No está habilitado, comunicarse con la liga")), JsonRequestBehavior.AllowGet);
            
            user.PasswordHash = userManager.PasswordHasher.HashPassword(vm.NuevoPassword);
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Json(JsonConvert.SerializeObject(ApiResponseCreator.Error("Hubo un error al cambiar la contraseña")), JsonRequestBehavior.AllowGet);
            
            usuarioDelegado.BlanqueoDeClavePendiente = false;
            usuarioDelegado.Password = vm.NuevoPassword;
            await context.SaveChangesAsync();
            
            return Json(JsonConvert.SerializeObject(ApiResponseCreator.Exito("Contraseña actualizada")), JsonRequestBehavior.AllowGet);
        }
        
		//
		// GET: /Account/VerifyCode
		[AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


		//GET: /Account/Register
		//[AllowAnonymous]
		//public ActionResult Register()
		//{
		//	return View();
		//}


		//POST: /Account/Register
		//[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		//public async Task<ActionResult> Register(RegisterViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
		//		var result = await UserManager.CreateAsync(user, model.Password);
		//		if (result.Succeeded)
		//		{
		//			//CUANDO QUIERA REGISTRAR UN USUARIO NUEVO
		//			//var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
		//			//var roleManager = new RoleManager<IdentityRole>(roleStore);
		//			//await roleManager.CreateAsync(new IdentityRole("SoloPuedeFichar"));
		//			//await UserManager.AddToRoleAsync(user.Id, "SoloPuedeFichar");

		//			//await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

		//			// For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
		//			// Send an email with this link
		//			// string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
		//			// var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
		//			// await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

		//			return RedirectToAction("Index", "Home");
		//		}
		//		AddErrors(result);
		//	}

		//	// If we got this far, something failed, redisplay form
		//	return View(model);
		//}

		//
		// GET: /Account/ConfirmEmail
		[AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
	        return RedirectToAction("MostrarPantallaDeCierreSesion");
        }

	    [AllowAnonymous]
		public ActionResult MostrarPantallaDeCierreSesion()
	    {
		    return View("CerrarSesionExito");
	    }

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Torneo");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}