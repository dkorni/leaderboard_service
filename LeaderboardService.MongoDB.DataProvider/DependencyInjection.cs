using LeaderboardService.Application.Interfaces;
using LeaderboardService.Domain;
using LeaderboardService.Domain.Exceptions;
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
                throw new InternalServerException("'MONGODB_URI' environment variable is not set.");
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