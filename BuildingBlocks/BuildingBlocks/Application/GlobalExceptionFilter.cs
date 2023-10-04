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
        _logger.LogError($"An Exception occurred of type: {context.Exception.GetType()}, with message: {context.Exception.Message}");
        
        context.ExceptionHandled = true;
        context.Result = exceptionParser(context.Exception);
        await context.Result.ExecuteResultAsync(context);
    }


    private IActionResult exceptionParser(Exception exception)
    {
        var response = new ObjectResult("An error occurred.");

        if (exception is DbUpdateException)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Value = "An error occurred while contacting the database.";
        }
        else if (exception is KeyNotFoundException)
        {
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Value = exception.Message;
        }
        else if (exception is UnauthorizedAccessException)
        {
            response.StatusCode = (int)HttpStatusCode.Unauthorized;
            response.Value = exception.Message;
        }
        else if (exception is ValidationException)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Value = exception.Message;
        }
        else if (exception is NullReferenceException)
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Value = "A null reference exception occurred.";
        }
        else
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Value = exception.Message;
        }

        return response;
    }
}