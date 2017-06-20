using Runniac.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Runniac.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "RunniacApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9a-f]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "RunniacApiByAction",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "Get" }
            );

            // register the exception logger and handler
            config.Services.Add(typeof(IExceptionLogger), new Log4NetExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // set error detail policy according with value from Web.config
            var customErrors =
              (CustomErrorsSection)ConfigurationManager.GetSection("system.web/customErrors");

            if (customErrors != null)
            {
                switch (customErrors.Mode)
                {
                    case CustomErrorsMode.RemoteOnly:
                        {
                            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

                            break;
                        }
                    case CustomErrorsMode.On:
                        {
                            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

                            break;
                        }
                    case CustomErrorsMode.Off:
                        {
                            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

                            break;
                        }
                    default:
                        {
                            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;

                            break;
                        }
                }
            }
        }
    }
}
