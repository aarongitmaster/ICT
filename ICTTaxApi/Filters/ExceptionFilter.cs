using Microsoft.AspNetCore.Mvc.Filters;

namespace ICTTaxApi.Filters
{
    public class ExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        //Para excepciones no controladas.
        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }

    }
}
