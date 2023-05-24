using ProductsPricing.Api.ApiResponses;
using ProductsPricing.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductsPricing.Api.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private const int DEFAULT_STATUS_CODE = 400;
        private const int NOT_FOUND_STATUS_CODE = 404;
        private const int FORBIDDEN_STATUS_CODE = 403;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ApiResponse response = null;
            int statusCode = DEFAULT_STATUS_CODE;

            try
            {
                await next(context);
                return;
            }
            catch (DomainException e)
            {
                response = ApiResponse.Error(e.ValidationFailuresMessages);
            }
            catch (Exception e)
            {
                statusCode = GetStatusCodeFromException(e);

                var exceptionsMessages = GetExceptionMessages(e);
                var exceptionsStackTrace = e.ToString() + (e.InnerException is not null ? Environment.NewLine + e.InnerException.ToString() : "");
                response = ApiResponse.Error(exceptionsMessages, exceptionsStackTrace);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private static IEnumerable<string> GetExceptionMessages(Exception e)
        {
            var exceptionMessages = new List<string>();

            exceptionMessages.Add(e.Message);

            if (e.InnerException is not null)
            {
                exceptionMessages.Add(e.InnerException.Message);
            }

            return exceptionMessages;
        }

        private int GetStatusCodeFromException(Exception e)
        {
            switch (e)
            {
                case NotFoundException:
                    return NOT_FOUND_STATUS_CODE;

                case NotAuthorizedException:
                    return FORBIDDEN_STATUS_CODE;

                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}