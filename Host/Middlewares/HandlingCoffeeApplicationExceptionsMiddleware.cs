using Domain.Exceptions;
using Host.Models.Errors;

namespace Host.Middlewares
{
    public class HandlingCoffeeApplicationExceptionsMiddleware
    {
        private readonly RequestDelegate next;

        public HandlingCoffeeApplicationExceptionsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (CoffeeApplicationException ex)
            {
                context.Response.StatusCode = ex.HttpStatusCode;
                var error = new CoffeeExceptionError(ex.MessageStatusCode, ex.Message);
                await context.Response.WriteAsJsonAsync(error);
            }
            catch
            {
                throw;
            }
        }
    }
}
