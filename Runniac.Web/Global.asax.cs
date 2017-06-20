using log4net;
using log4net.Config;
using Runniac.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

namespace Runniac.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperWebConfiguration.Configure();
            DependenciesBootstrapper.Run();
            XmlConfigurator.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
            DatabaseConfig.Setup();
            AuthConfig.RegisterAuth();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Error()
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                //Redirect HTTP errors to HttpError page
                Server.Transfer("Errors/HttpErrorPage.html");
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            Response.Write("<h2>Global Page Error</h2>\n");
            Response.Write(
                "<p>" + exc.Message + "</p>\n");
            Response.Write("Return to the <a href='/'>" +
                "Home Page</a>\n");

            // Log the exception 
            log.Error(string.Format("[App_Error] {0}", exc.Message), exc);

            // Clear the error from the server
            Server.ClearError();

        }
    }
}
