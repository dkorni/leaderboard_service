namespace LeaderboardService.Domain;

public record PagedResponse<T>(
    int page,
    int pageSize,
    int totalPages,
    int totalRecords,
    T[] records
    );