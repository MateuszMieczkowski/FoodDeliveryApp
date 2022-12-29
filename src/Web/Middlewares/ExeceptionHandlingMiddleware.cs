using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using Web.Api.Exceptions;

namespace Web.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch(NotFoundException ex)
			{
                ProblemDetails problemDetails = new ProblemDetails()
                {
                    Title = "Not found",
                    Status = (int)HttpStatusCode.NotFound,
					Detail = ex.Message
                };
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
			catch(BadRequestException ex)
			{
                ProblemDetails problemDetails = new ProblemDetails()
                {
                    Title = "Bad request",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ex.Message
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
			catch (Exception)
			{
				
				ProblemDetails problemDetails = new ProblemDetails()
				{
					Title = "Internal server Error",
					Status = (int)HttpStatusCode.InternalServerError
				};
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsJsonAsync(problemDetails);
			}
        }
    }
}
