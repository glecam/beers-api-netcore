using System;
using System.Net;
using System.Threading.Tasks;
using Beers.API.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Beers.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.BadRequest; // 400 if unexpected

            if (exception is DllNotFoundException || exception is NotFoundException) code = HttpStatusCode.NotFound;

            var hostingEnvironment = (IHostingEnvironment)context.RequestServices.GetService(typeof(IHostingEnvironment));

            var response = hostingEnvironment.IsDevelopment() 
                ? JsonConvert.SerializeObject(new { error = exception.Message, stackTrace = exception.StackTrace, innerException = exception.InnerException?.ToString() }) 
                : JsonConvert.SerializeObject(new { error = exception.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(response);
        }
    }
}
