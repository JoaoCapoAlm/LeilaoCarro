using System.Net;
using System.Text.Json;
using FluentValidation;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Exceptions;

namespace LeilaoCarro.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                ValidationException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                AppException => ((AppException)ex).StatusCode,
                _ => HttpStatusCode.InternalServerError
            };

            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
            string title = ex.Message;
            if (!isDevelopment && code.Equals(HttpStatusCode.InternalServerError))
                title = "Erro Interno do Servidor";
            else if (isDevelopment && ex.InnerException != null)
                title = $"{ex.Message} - {ex.InnerException}";

            var responseBody = new ExceptionVM(title.TrimEnd(' ', '-'), code.GetHashCode());

            if (ex.GetType() == typeof(AppException))
                responseBody.Errors = ((AppException)ex).Erros;

            var result = JsonSerializer.Serialize(responseBody);
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
