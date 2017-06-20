using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.GoogleOAuth2;
using Microsoft.Web.WebPages.OAuth;
using Runniac.Business;
using Runniac.Data;
using Runniac.Email;
using Runniac.Filters;
using Runniac.Membership;
using Runniac.Membership.Models;
using Runniac.Web.App_Start.MappingProfiles;
using Runniac.Web.ExternalLoginParsers;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Runniac.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUsersContext _usersDbContext;
        private IEmailSender _emailSender;
        private IUserService _userService;

        public AccountController(IUsersContext usersDbContext, IEmailSender emailSender, IUserService userService)
        {
            _usersDbContext = usersDbContext;
            _emailSender = emailSender;
            _userService = userService;
        }       

        //
        // GET: /Account/Login
        [RequireHttps]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [RequireHttps]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                var user = _userService.GetByUserName(model.UserName);
                Session["UserFullName"] = user.Name != null ? String.Format("{0} {1}", user.Name, user.Lastname) : user.UserName;

                if (Roles.IsUserInRole(model.UserName, "Administrator"))
                    return RedirectToAction("Index", "Admin");

                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Email o contraseña incorrectos.");
            return View(model);
        }

        //
        // GET: /Account/LogOff
        [RequireHttps]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            Session.Remove("UserFullName");

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [RequireHttps]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [RequireHttps]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var confirmationToken = WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                        new { Name = model.Name, Lastname = model.Lastname }, true);
                    var user = _usersDbContext.UserProfiles.FirstOrDefault(u => u.UserName == model.Email);
                    _userService.SaveUser(new User { Id = user.UserId, UserName = model.Email, Name = model.Name, Lastname = model.Lastname });

                    //Creación de una URL con el token
                    var confirmationUrl = "<a href='" + Url.Action("ConfirmAccount", "Account", new { token = confirmationToken }, "https") +
                        "'>Confirmar registro</a>";

                    //Envío del email

                    var subject = "[Runniac] Confirma tu registro";
                    var body = String.Format("{0}{1}", "Por favor, haz click en el siguiente enlace para confirmar tu cuenta de usuario:<br/>",
                        confirmationUrl);
                    try
                    {
                        _emailSender.SendMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(),
                        model.Email, subject, body);
                        TempData["Message"] = "Email de confirmación enviado. Por favor revisa tu e-mail para finalizar el proceso de registro";
                        TempData["Result"] = "has-success";
                    }
                    catch (Exception)
                    {
                        TempData["Message"] = "Se ha producido un error al intentar enviar el email";
                        TempData["Result"] = "has-error";
                    }

                    return View();
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [RequireHttps]
        [AllowAnonymous]
        public ActionResult ConfirmAccount(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                if (WebSecurity.ConfirmAccount(token))
                {
                    TempData["Result"] = "has-success";
                    TempData["Message"] = "Tu cuenta ha sido confirmada. ¡Ya puedes hacer login en Runniac!";
                }
                else
                {
                    TempData["Result"] = "has-error";
                    TempData["Message"] = "There was problem while confirming your account.";
                }
            }
            return View();
        }

        [RequireHttps]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [RequireHttps]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string UserName)
        {
            //Comprobación de si el usuario existe
            var user = _usersDbContext.UserProfiles.Where(
                u => u.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (user == null)
            {
                TempData["Message"] = "Email no encontrado";
                TempData["Result"] = "has-error";
            }
            else
            {
                //Generación de token
                var token = WebSecurity.GeneratePasswordResetToken(UserName);
                //Creación de una URL con el token
                var resetLink = "<a href='" + Url.Action("ResetPassword", "Account", new { un = UserName, rt = token }, "http") +
                    "'>Resetear contraseña</a>";

                //Envío del email

                var subject = "[Runniac] Resetea tu contraseña";
                var body = String.Format("{0}{1}", "Por favor, haz click en el siguiente enlace para resetear tu contraseña:<br/>",
                    resetLink);
                try
                {
                    _emailSender.SendMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(),
                    user.UserName, subject, body);
                    TempData["Message"] = "Email enviado";
                }
                catch (Exception)
                {
                    TempData["Message"] = "Se ha producido un error al intentar enviar el email";
                }
            }

            return View();
        }

        [RequireHttps]
        [AllowAnonymous]
        public ActionResult ResetPassword(string un, string rt)
        {
            var userid = _usersDbContext.UserProfiles.Where(u => u.UserName == un).Select(u => u.UserId).FirstOrDefault();
            bool any = _usersDbContext.Memberships.Any(m => m.UserId == userid && m.PasswordVerificationToken == rt);

            if (any)
            {
                string newpassword = GenerateRandomPassword(6);
                bool response = WebSecurity.ResetPassword(rt, newpassword);

                if (response)
                {
                    //send email
                    string subject = "[Runniac] Su nueva contraseña";
                    string body = String.Format("Su nueva contraseña es <strong>{0}</strong>", newpassword);
                    try
                    {
                        _emailSender.SendMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), un, subject, body);
                        TempData["Message"] = "Le hemos enviado su nueva contraseña por email";
                    }
                    catch (Exception)
                    {
                        TempData["Message"] = "Error al intentar enviar el email";
                        TempData["Result"] = "has-error";
                    }
                }
            }
            else
            {
                TempData["Message"] = "El email y el token no coinciden con un usuario registrado";
                TempData["Result"] = "has-error";
            }

            return View();
        }

        /// <summary>
        /// Genera una contraseña aleatoria.
        /// </summary>
        /// <param name="length">Nº de caracteres que se desea que tenga la contraseña generada.</param>
        /// <returns>La nueva contraseña.</returns>
        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*&#+";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }


        #endregion

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {           
            GoogleOAuth2Client.RewriteRequest();
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                ILoginParser loginParser;

                if (ViewBag.ProviderDisplayName == "Google")
                {
                    loginParser = new GoogleLoginParser();
                    return View("ExternalLoginConfirmation", loginParser.Parse(result, loginData));

                }
                else if (ViewBag.ProviderDisplayName == "Facebook")
                {
                    loginParser = new FacebookLoginParser();
                    return View("ExternalLoginConfirmation", loginParser.Parse(result, loginData));
                }
                else
                {
                    return RedirectToAction("ExternalLoginFailure");
                }
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserInfo user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.Email.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        var u = new UserInfo { UserName = model.Email, Name = model.Name, Lastname = model.Lastname };
                        db.UserProfiles.Add(u);
                        db.SaveChanges();
                        _userService.SaveUser(new User { Id = u.UserId, UserName = model.Email, Name = model.Name, Lastname = model.Lastname });                       

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.Email);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Este email ya está en uso. Prueba a registrarte con otra cuenta");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

    }
}
