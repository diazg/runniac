using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.ExternalLoginParsers
{
    public class FacebookLoginParser : ILoginParser
    {
        public RegisterExternalLoginModel Parse(DotNetOpenAuth.AspNet.AuthenticationResult result, string loginData)
        {
            var name = new string[2];
            if (!String.IsNullOrEmpty(result.ExtraData["name"]))
            {
                var aux = result.ExtraData["name"].IndexOf(' ');
                name[0] = result.ExtraData["name"].Substring(0, aux);
                name[1] = result.ExtraData["name"].Substring(aux + 1, result.ExtraData["name"].Length - aux - 1);
            }

            return new RegisterExternalLoginModel
                {
                    Email = result.UserName,
                    Name = name[0],
                    Lastname = name[1],
                    ExternalLoginData = loginData
                };
        }
    }
}