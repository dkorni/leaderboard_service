// See https://aka.ms/new-console-template for more information

using LeaderboardService.Application;
using LeaderboardService.EventHandlers;
using LeaderboardService.MongoDB.DataProvider;
using Microsoft.Extensions.Hosting;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMongoDbDataProvider();
        services.AddApplication();
        services.AddEventListener();
    })
    .UseSerilog((context, loggerConfig) =>
        loggerConfig.ReadFrom.Configuration(context.Configuration))
    .Build();

await host.RunAsync();
