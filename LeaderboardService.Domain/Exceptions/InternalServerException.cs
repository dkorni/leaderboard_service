namespace LeaderboardService.Domain.Exceptions;

public class InternalServerException(string message) : Exception(message);