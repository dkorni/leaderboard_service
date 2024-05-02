using LeaderboardService.Domain;
using Microsoft.EntityFrameworkCore;
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
        modelBuilder.Entity<LeaderBoardMemberModel>().ToCollection("leaderBoardMembers");
        
        base.OnModelCreating(modelBuilder);
    }
}