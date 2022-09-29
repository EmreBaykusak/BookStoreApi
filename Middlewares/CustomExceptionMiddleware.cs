using System.Diagnostics;
using System.Net;
using BookStoreApi.Services;
using Newtonsoft.Json;

namespace BookStoreApi.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        
        try
        {
            string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            _logger.Write(message);
            await _next(context);
            watch.Stop();

            message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " +
                      context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds + " ms";
            _logger.Write(message);
        }
        catch (Exception ex)
        {
           watch.Stop();

           await HandleException(context, ex, watch);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
    {
        string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode 
                         + " Error Message " + exception.Message + " in " + watch.Elapsed.Milliseconds + " ms";
        
        _logger.Write(message);
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new { error = exception.Message },Formatting.None);
        context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtension
{   
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    } 
}