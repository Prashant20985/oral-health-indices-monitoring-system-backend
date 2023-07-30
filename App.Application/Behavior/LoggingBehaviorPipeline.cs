using App.Application.AccountOperations;
using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Behavior;

public class LoggingBehaviorPipeline<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviorPipeline<TRequest, TResponse>> _logger;
    private readonly IHttpContextAccessorService _httpContextAccessorService;

    public LoggingBehaviorPipeline(
        ILogger<LoggingBehaviorPipeline<TRequest, TResponse>> logger,
        IHttpContextAccessorService httpContextAccessorService)
    {
        _logger = logger;
        _httpContextAccessorService = httpContextAccessorService;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {

        if (ShouldExcludeLogging(request))
        {
            return await next();
        }

        _logger.LogInformation("Starting Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
            _httpContextAccessorService.GetUserName() ?? string.Empty,
            typeof(TRequest).Name,
            DateTime.UtcNow);

        TResponse response;

        try
        {
            response = await next();

            if (response is not null && typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                bool isSuccessful = (bool)typeof(TResponse).GetProperty("IsSuccessful")?.GetValue(response);
                string errorMessage = (string)typeof(TResponse).GetProperty("ErrorMessage")?.GetValue(response);

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
            _logger.LogCritical("Exception on Request {@ExecutedBy}, {@RequestName}, {@ErrorMessage}, {@DateTimeUtc}",
                _httpContextAccessorService.GetUserName() ?? string.Empty,
                typeof(TRequest).Name,
                ex.Message,
                DateTime.UtcNow);

            response = await next();
        }


        _logger.LogInformation("Completed Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
            _httpContextAccessorService.GetUserName() ?? string.Empty,
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return response;
    }

    private static bool ShouldExcludeLogging(TRequest request)
    {
        return
            //  request is Login.LoginCommand ||
            request is ChangePassword.ChangePasswordCommand ||
            request is ResetPassword.ResetPasswordCommand ||
            request is ResetPasswordEmailRequest.ResetPasswordEmailRequestCommand ||
            request is CurrentUser.CurrentUserQuery ||
            request is RefreshToken.RefreshTokenCommand ||
            request is EmailNotification.Email;
    }
}
