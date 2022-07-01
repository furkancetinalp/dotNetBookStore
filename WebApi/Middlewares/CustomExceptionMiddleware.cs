
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using WebApi.Services;
namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerservice;

        public CustomExceptionMiddleware(RequestDelegate next,ILoggerService loggerservice)
        {
            _next=next;
            _loggerservice=loggerservice;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] Http "+context.Request.Method+"-"+context.Request.Path;
                _loggerservice.Write(message);
                //Console.WriteLine(message);
                await _next(context);

                watch.Stop();
                message="[Response]   Http "+context.Request.Method+"-"+context.Request.Path+" responded "+context.Response.StatusCode+" in "+watch.Elapsed.TotalMilliseconds+" ms";
                _loggerservice.Write(message);
                //System.Console.WriteLine(message);
            }
            catch(Exception ex)
            {
                watch.Stop();
                await HandleException(context,ex,watch);
            }
                       
        }
        private Task HandleException(HttpContext context,Exception ex,Stopwatch watch)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            
            string message = "[Error]    Http "+context.Request.Method+"-"+context.Response.StatusCode+" Error Message: "+ex.Message+" in "+watch.Elapsed.TotalMilliseconds+" ms";
            _loggerservice.Write(message);
            //System.Console.WriteLine(message);

            var result = JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

}