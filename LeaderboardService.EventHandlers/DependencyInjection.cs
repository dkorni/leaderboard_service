using LeaderboardService.Contracts.Events;
using LeaderboardService.EventHandlers.Handlers;
using LeaderboardService.EventHandlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardService.EventHandlers;

public static class DependencyInjection
{
    public static void AddEventListener(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IEventHandler<UpdateUserScoreEventData>, UpdateUserScoreEventDataHandler>();
        
        serviceCollection.AddSingleton<IEventDispatcher, EventDispatcher.EventDispatcher>(sp => 
            new EventDispatcherBuilder(sp)
                .AddEventHandler<IEventHandler<UpdateUserScoreEventData>, UpdateUserScoreEventData>()
                .Build());

        serviceCollection.AddHostedService<EventListenerService>();
    }
}