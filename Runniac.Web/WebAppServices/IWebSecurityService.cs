using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Web.WebAppServices
{
    public interface IWebSecurityService
    {
        int CurrentUserId { get; }
    }
}
