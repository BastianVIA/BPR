using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application;

public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        _logger.LogError(
            $"An Exception occurred of type: {context.Exception.GetType()}, with message: {context.Exception.Message}");

        context.ExceptionHandled = true;
        context.Result = exceptionParser(context.Exception);
        await context.Result.ExecuteResultAsync(context);
    }


    private IActionResult exceptionParser(Exception exception)
    {
        var response = new ObjectResult("An error occurred.");

        switch (exception)
        {
            case DbUpdateException _:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Value = "An error occurred while contacting the database.";
                break;

            case KeyNotFoundException keyNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Value = keyNotFoundException.Message;
                break;

            case UnauthorizedAccessException unauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Value = unauthorizedAccessException.Message;
                break;

            case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Value = validationException.Message;
                break;

            case NullReferenceException _:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Value = "A null reference exception occurred.";
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Value = exception.Message;
                break;
        }


        return response;
    }
}