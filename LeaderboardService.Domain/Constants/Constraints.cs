namespace LeaderboardService.Domain.Constants;

public static class Constraints
{
    public static readonly int MinId = 1;
    public static readonly int MaxId = 1000000;
    
    public static readonly int MinUserId = 1;
    public static readonly int MaxUserId = 1000000;
    
    public static readonly int MinScore = 1;
    public static readonly int MaxScore = 100;
}