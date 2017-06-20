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
    public class HomeController : BaseController
    {

        public HomeController(IUserService userService)
            : base(userService)
        { }

        //
        // GET: /Home/
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }
    }
}