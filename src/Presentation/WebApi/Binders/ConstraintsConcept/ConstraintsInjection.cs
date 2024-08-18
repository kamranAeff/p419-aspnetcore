namespace WebApi.Binders.ConstraintsConcept
{
    public static class ConstraintsInjection
    {
        public static IServiceCollection InjectConstraints(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(cfg =>
            {
                cfg.ConstraintMap.TryAdd("slug", typeof(SlugRouteConstraint));
            });

            return services;
        }
    }
}
