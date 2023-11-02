using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application;

public class GlobalExceptionFilter
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(RequestDelegate next, ILogger<GlobalExceptionFilter> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                $"An Exception occurred of type: {exception.GetType()}, with message: {exception.Message}");

            context.Response.Clear();

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.Message
            };

            switch (exception)
            {
                case DbUpdateException _:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = "An error occurred while contacting the database.";
                    break;

                case KeyNotFoundException keyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    problemDetails.Title = "Resource not found";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Detail = keyNotFoundException.Message;
                    break;

                case UnauthorizedAccessException unauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Title = "Unauthorized";
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Detail = unauthorizedAccessException.Message;
                    break;

                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Validation error";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = validationException.Message;
                    break;

                case NullReferenceException _:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = "A null reference exception occurred.";
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}