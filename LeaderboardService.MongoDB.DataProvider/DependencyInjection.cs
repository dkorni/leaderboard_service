using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace LeaderboardService.MongoDB.DataProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDbDataProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContextFactory<MongoDbContext>((optionBuilder) =>
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            
            if (connectionString == null)
            {
                // todo log it and throw exception
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }
            
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("leaderboard-db");

            optionBuilder.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
        });

        serviceCollection
            .AddSingleton<IRepository<LeaderBoardMemberModel>, MongoDbRepository<LeaderBoardMemberModel>>();
        
        return serviceCollection;
    }
}