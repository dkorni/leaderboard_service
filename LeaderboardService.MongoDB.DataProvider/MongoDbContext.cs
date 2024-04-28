using LeaderboardService.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace LeaderboardService.MongoDB.DataProvider;

public class MongoDbContext : DbContext
{
    public DbSet<LeaderBoardMemberModel> LeaderBoardMembers { get; set; }
    
    public MongoDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<LeaderBoardMemberModel>().ToCollection("leaderBoardMembers");
    }
}