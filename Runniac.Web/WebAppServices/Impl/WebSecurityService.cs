using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Runniac.Web.WebAppServices.Impl
{
    public class WebSecurityService : IWebSecurityService
    {
        public int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }
    }
}