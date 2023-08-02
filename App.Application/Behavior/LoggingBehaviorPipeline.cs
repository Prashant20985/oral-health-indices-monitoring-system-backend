using App.Application.AccountOperations.ChangePassword;
using App.Application.AccountOperations.CurrentUser;
using App.Application.AccountOperations.Login;
using App.Application.AccountOperations.RefreshToken;
using App.Application.AccountOperations.ResetPassword;
using App.Application.AccountOperations.ResetPasswordUrlEmailRequest;
using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Behavior
{
    /// <summary>
    /// Pipeline behavior for logging requests and responses in the MediatR pipeline.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being processed.</typeparam>
    /// <typeparam name="TResponse">The type of the response produced by the pipeline.</typeparam>
    public class LoggingBehaviorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehaviorPipeline<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessorService _httpContextAccessorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehaviorPipeline{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger used to log information.</param>
        /// <param name="httpContextAccessorService">The service for accessing the HttpContext.</param>
        public LoggingBehaviorPipeline(
            ILogger<LoggingBehaviorPipeline<TRequest, TResponse>> logger,
            IHttpContextAccessorService httpContextAccessorService)
        {
            _logger = logger;
            _httpContextAccessorService = httpContextAccessorService;
        }

        /// <summary>
        /// Handles the request by logging relevant information and passing it to the next handler in the pipeline.
        /// </summary>
        /// <param name="request">The request being processed.</param>
        /// <param name="next">The delegate representing the next handler in the pipeline.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>The response produced by the pipeline.</returns>
        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (ShouldExcludeLogging(request))
            {
                return await next();
            }

            // Log the start of the request
            _logger.LogInformation("Starting Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
                _httpContextAccessorService.GetUserName() ?? string.Empty,
                typeof(TRequest).Name,
                DateTime.UtcNow);

            TResponse response;

            try
            {
                // Execute the next handler in the pipeline and get the response
                response = await next();

                // If the response is of type OperationResult<> and not null
                if (response is not null && typeof(TResponse).IsGenericType &&
                    typeof(TResponse).GetGenericTypeDefinition() == typeof(OperationResult<>))
                {
                    // Extract the IsSuccessful and ErrorMessage properties from the response
                    bool isSuccessful = (bool)typeof(TResponse).GetProperty("IsSuccessful")?.GetValue(response);
                    string errorMessage = (string)typeof(TResponse).GetProperty("ErrorMessage")?.GetValue(response);

                    // If the operation was not successful, log the error message
                    if (!isSuccessful)
                    {
                        _logger.LogError("Request Failure {@ExecutedBy}, {@RequestName}, {@ErrorMessage}, {@DateTimeUtc}",
                            _httpContextAccessorService.GetUserName() ?? string.Empty,
                            typeof(TRequest).Name,
                            errorMessage,
                            DateTime.UtcNow);
                    }
                }
            }
            catch (Exception ex)
            {
                // If an exception occurred during request processing, log the critical error
                _logger.LogCritical("Exception on Request {@ExecutedBy}, {@RequestName}, {@ErrorMessage}, {@DateTimeUtc}",
                    _httpContextAccessorService.GetUserName() ?? string.Empty,
                    typeof(TRequest).Name,
                    ex.Message,
                    DateTime.UtcNow);

                // Continue to the next handler to handle the exception further
                response = await next();
            }

            // Log the completion of the request
            _logger.LogInformation("Completed Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
                _httpContextAccessorService.GetUserName() ?? string.Empty,
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return response; // Return the final response to the calling code
        }

        /// <summary>
        /// Determines whether logging should be excluded for certain types of requests.
        /// </summary>
        /// <param name="request">The request to check.</param>
        /// <returns><c>true</c> if logging should be excluded for the request; otherwise, <c>false</c>.</returns>
        private static bool ShouldExcludeLogging(TRequest request)
        {
            return
                request is LoginCommand || // Exclude login commands
                request is ChangePasswordCommand || // Exclude change password commands
                request is ResetPasswordCommand || // Exclude reset password commands
                request is ResetPasswordUrlEmailRequestCommand || // Exclude reset password URL email requests
                request is CurrentUserQuery || // Exclude current user queries
                request is RefreshTokenCommand || // Exclude refresh token commands
                request is EmailNotification; // Exclude email notification requests
        }
    }
}
