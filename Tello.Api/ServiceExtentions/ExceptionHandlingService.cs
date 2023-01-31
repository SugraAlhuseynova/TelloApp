using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Tello.Service.Exceptions;

namespace Tello.Api.ServiceExtentions
{
    public static class ExceptionHandlingService
    {
        public static void ExceptionHandler(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();

                    int statusCode = 500;
                    string message = "Internal server error";
                    if (contextFeatures != null)
                    {
                        message = contextFeatures.Error.Message;
                        if (contextFeatures.Error is ItemNotFoundException)
                        {
                            statusCode = 404;
                        }
                        if (contextFeatures.Error is RecordDuplicatedException)
                        {
                            statusCode = 409;
                        }
                    }

                    context.Response.StatusCode = statusCode;
                    string responseStr = JsonConvert.SerializeObject(new
                    {
                        Message = message,
                        Code = statusCode
                    });
                    await context.Response.WriteAsync(responseStr);
                });
            });

        }
    }
}
