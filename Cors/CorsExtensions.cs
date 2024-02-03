namespace GameStore.Cors
{
    public static class CorsExtensions
    {
        private const string allowedOriginSetting = "AllowedOrigin";

        public static IServiceCollection AddGameStoreCors(
            this IServiceCollection services, IConfiguration configuration)
        {
            // Add CORS also we should Use Cors in Middleware
            return services.AddCors(opt =>
             {
                 opt.AddDefaultPolicy(corsBuilder =>
                 {
                     var allowedOrigin = configuration[allowedOriginSetting]
                         ?? throw new InvalidOperationException("Allowed origin is not set");

                     corsBuilder
                         .WithOrigins(allowedOrigin)
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                 });
             });
        }
    }
}
