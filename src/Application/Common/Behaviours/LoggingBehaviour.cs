using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request : {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Response : {JsonConvert.SerializeObject(response)}");

            return response;
        }
    }
}
