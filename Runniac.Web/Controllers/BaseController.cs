using Runniac.Business;
using Runniac.Data.Repositories;
using Runniac.Email;
using Runniac.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Runniac.Web.Controllers
{
    public class BaseController : Controller
    {
        private IUserService _userService;

        public BaseController(IUserService userService)
        {
            _userService = userService;
            GetUserFullName();
        }

        /// <summary>
        /// Inserta en sesión el nombre completo del usuario si la sesión caduca.
        /// </summary>
        private void GetUserFullName()
        {
            if (System.Web.HttpContext.Current.Session["UserFullName"] == null)
            {
                var user = _userService.GetById(WebSecurity.CurrentUserId);
                if (user != null)
                    System.Web.HttpContext.Current.Session["UserFullName"] = String.Format("{0} {1}", user.Name, user.Lastname);
            }
        }
    }
}