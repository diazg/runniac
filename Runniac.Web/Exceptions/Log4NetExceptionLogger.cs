using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Runniac.Web.Exceptions
{
    class Log4NetExceptionLogger : ExceptionLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));


        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var exc = context.Exception;
            log.Error(string.Format("[App_Error] {0}", exc.Message), exc);
            return Task.FromResult(0);
        }
    }
}