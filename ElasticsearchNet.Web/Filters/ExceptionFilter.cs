using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElasticsearchNet.Web.Filters
{
    public class ExceptionFilter : IActionFilter
    {
        private readonly ErrorHandlerOptions errOpts;

        public ExceptionFilter(Action<ErrorHandlerOptions> opts)
        {
            var errorHandlerOptions = new ErrorHandlerOptions();
            opts(errorHandlerOptions);
            this.errOpts = errorHandlerOptions;
        }

        public ExceptionFilter()
        {
            this.errOpts = new ErrorHandlerOptions();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var exception = context.Exception;

            if (exception != null)
            {
                var type = exception.GetType();

                if (errOpts.Handlers.ContainsKey(type))
                {
                    errOpts.Handlers[type].Handle(exception, context.HttpContext.Response);
                } 
                else 
                {
                    new GenericErrorHandler().Handle(exception, context.HttpContext.Response);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {}
    }
}
