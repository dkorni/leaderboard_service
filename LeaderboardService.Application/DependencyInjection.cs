using LeaderboardService.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
        return serviceCollection;
    }
}