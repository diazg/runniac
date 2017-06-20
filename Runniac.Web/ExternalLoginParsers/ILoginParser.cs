using DotNetOpenAuth.AspNet;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Web.ExternalLoginParsers
{
    public interface ILoginParser
    {
        RegisterExternalLoginModel Parse(AuthenticationResult result, string loginData);
    }
}
