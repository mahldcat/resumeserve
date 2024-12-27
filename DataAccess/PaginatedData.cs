using System.Data;
using Dapper;
using DataAccess.Poco;
// ReSharper disable UseRawString

namespace DataAccess;

public class PaginatedData(PaginatedDataConnection connection)
{
    private readonly IDbConnection _dbConnection = connection.Connection;
    
    /// <summary>
    /// Fetch a PaginatedDataEntry by Id.
    /// </summary>
    public async Task<PaginatedDataEntry> FetchByIdAsync(int id)
    {
        string sql = "SELECT * FROM PaginatedDataEntry WHERE Id = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<PaginatedDataEntry>(sql, new { Id = id });
    }

    /// <summary>
    /// Fetch a list of PaginatedDataEntry based on Id (pagination).
    /// </summary>
    public async Task<(IList<PaginatedDataEntry> Entries, int TotalCount)> FetchByIdAsync(int page, int maxPageSize)
    {
        int offset = (page - 1) * maxPageSize;

        // Query to fetch the data from the offset
        string dataQuery = @"
            SELECT * 
            FROM PaginatedData 
            ORDER BY Id 
            OFFSET @Offset ROWS 
            FETCH NEXT @MaxPageSize ROWS ONLY";

        // Query to fetch the total count
        string countQuery = @"
        SELECT COUNT(*) 
        FROM PaginatedData 
        WHERE Updated >= @Date OR Created >= @Date";

        using var multi = await _dbConnection.QueryMultipleAsync($"{dataQuery};{countQuery}", new
        {
            Offset = offset,
            MaxPageSize = maxPageSize
        });

        var entries = (await multi.ReadAsync<PaginatedDataEntry>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        return (entries, totalCount);
    }

    public async Task<(IList<PaginatedDataEntry> Entries, int TotalCount)> FetchByUpdatedOrCreatedAsync(DateTime date,
        int page, int maxPageSize)
    {
        int offset = (page - 1) * maxPageSize;

        // Query to fetch the paginated results
        string dataQuery = @"
        SELECT * 
        FROM PaginatedData 
        WHERE Updated >= @Date OR Created >= @Date
        ORDER BY Updated DESC, Created DESC
        OFFSET @Offset ROWS FETCH NEXT @MaxPageSize ROWS ONLY";

        // Query to fetch the total count
        string countQuery = @"
        SELECT COUNT(*) 
        FROM PaginatedData 
        WHERE Updated >= @Date OR Created >= @Date";

        using var multi = await _dbConnection.QueryMultipleAsync($"{dataQuery};{countQuery}", new
        {
            Date = date,
            Offset = offset,
            MaxPageSize = maxPageSize
        });

        var entries = (await multi.ReadAsync<PaginatedDataEntry>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        return (entries, totalCount);
    }
}