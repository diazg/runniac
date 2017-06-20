using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.ExternalLoginParsers
{
    public class GoogleLoginParser : ILoginParser
    {
        public RegisterExternalLoginModel Parse(DotNetOpenAuth.AspNet.AuthenticationResult result, string loginData)
        {
            return new RegisterExternalLoginModel
                {
                    Email = result.ExtraData["email"],
                    Name = result.ExtraData["given_name"],
                    Lastname = result.ExtraData["family_name"],
                    ExternalLoginData = loginData
                };
        }
    }
}