using System.Net;
using API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (NotFoundException ex)
		{
			var problemDetails = new ProblemDetails
			{
				Title = "Not found",
				Status = (int) HttpStatusCode.NotFound,
				Detail = ex.Message
			};
			context.Response.StatusCode = (int) HttpStatusCode.NotFound;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
		catch (BadRequestException ex)
		{
			var problemDetails = new ProblemDetails
			{
				Title = "Bad request",
				Status = (int) HttpStatusCode.BadRequest,
				Detail = ex.Message
			};
			context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
		catch (ForbiddenException ex)
		{
			var problemDetails = new ProblemDetails
			{
				Title = "Forbidden",
				Status = (int) HttpStatusCode.Forbidden,
				Detail = ex.Message
			};
			context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
		catch (Exception)
		{
				
			var problemDetails = new ProblemDetails
			{
				Title = "Internal server Error",
				Status = (int)HttpStatusCode.InternalServerError
			};
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
	}
}