using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace UserManagement.Controllers.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await WriteProblem(context, (int)HttpStatusCode.NotFound, "Not Found", ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await WriteProblem(context, (int)HttpStatusCode.BadRequest, "Invalid Operation", ex.Message);
            }
            catch (Exception ex)
            {
                await WriteProblem(context, (int)HttpStatusCode.InternalServerError, "Server Error", ex.Message);
            }
        }

        private static Task WriteProblem(HttpContext ctx, int status, string title, string detail)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = status;
            var problem = new ProblemDetails { Status = status, Title = title, Detail = detail };
            return ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
