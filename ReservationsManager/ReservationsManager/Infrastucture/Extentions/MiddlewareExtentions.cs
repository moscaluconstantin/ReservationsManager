using ReservationsManager.API.Infrastucture.Middlewares;

namespace ReservationsManager.API.Infrastucture.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app) => 
            app.UseMiddleware<ExceptionHandlingMiddleware>();

        public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
            => app.UseMiddleware<DbTransactionMiddleware>();
    }
}
