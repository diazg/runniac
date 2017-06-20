using DotNetOpenAuth.GoogleOAuth2;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Runniac.Web.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            var client = new GoogleOAuth2Client(
                ConfigurationManager.AppSettings["GoogleAppId"],
                ConfigurationManager.AppSettings["GoogleAppSecret"]);
            var extraData = new Dictionary<string, object>();
            OAuthWebSecurity.RegisterClient(client, "Google", extraData);

            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings["FacebookAppId"],
                appSecret: ConfigurationManager.AppSettings["FacebookAppSecret"]);
        }
    }
}