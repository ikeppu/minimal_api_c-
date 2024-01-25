namespace GameStore.Authorization
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddGameStoreAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(o =>
            {
                o.AddPolicy(Policies.ReadAccess, builder =>
                {
                    builder.RequireClaim("scope", "games:read");
                });
                o.AddPolicy(Policies.WriteAccess, builder =>
                {
                    builder.RequireClaim("scope", "games:write")
                        .RequireRole("Admin");
                });
            });

            return services;
        }
    }
}
