using Runniac.Business;
using Runniac.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runniac.Web.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        public AdminController(IUserService userService)
            : base(userService)
        { }

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            var url = UrlHelper.GenerateUrl(
               null,
               "Manage",
               "Admin",
               null,
               null,
               "listEvents",
               null,
               Url.RouteCollection,
               Url.RequestContext,
               false
           );
            return Redirect(url);
        }

        public ActionResult Manage()
        {
            return View("Index");
        }
    }
}
