namespace E_Commerece.API.ExceptionsConfiguration
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder AddGlobalErrorHandlingMiddlewares(this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionsHandlingMidlleWare>();
    }
}
