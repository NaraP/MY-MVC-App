using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace MVCDIWebApplication.ErrorLogger
{
    public class ExceptionLoggerFilter : IExceptionFilter
    {
        private static ExceptionLogging _Logger = null;

        public ExceptionLoggerFilter(ExceptionLogging logger)
        {
            _Logger = logger;
        }

        public bool AllowMultiple { get { return true; } }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                _Logger.SendErrorToText(actionExecutedContext.Exception);
            }, cancellationToken);
        }
    }
}