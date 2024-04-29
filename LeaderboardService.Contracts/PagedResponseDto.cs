namespace LeaderboardService.Contracts;

public record PagedResponseDto<T>(
    int page,
    int pageSize,
    int totalPages,
    int totalRecords,
    T[] records
    );