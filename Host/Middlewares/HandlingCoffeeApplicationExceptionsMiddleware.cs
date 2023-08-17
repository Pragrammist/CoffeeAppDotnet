using Domain.Exceptions;

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
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsync(ex.Message);
            }
            catch
            {
                throw;
            }
        }
    }
}
