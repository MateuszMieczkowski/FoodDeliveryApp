using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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
			catch (Exception ex)
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
