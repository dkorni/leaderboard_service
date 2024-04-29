namespace LeaderboardService.Domain.Exceptions;

public class ValidationException(string message) : Exception(message);