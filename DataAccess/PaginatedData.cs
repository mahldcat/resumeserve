using System.Data;
using Dapper;
using DataAccess.Poco;
// ReSharper disable UseRawString

namespace DataAccess;

public class PaginatedData(PaginatedDataConnection connection) : IPaginatedData
{
    private readonly IDbConnection _dbConnection = connection.Connection;
    
    /// <summary>
    /// Fetch a PaginatedDataEntry by Id.
    /// </summary>
    public async Task<PaginatedDataEntry> FetchByIdAsync(int id)
    {
        string sql = "SELECT * FROM PaginatedDataEntry WHERE Id = @Id";
        return (await _dbConnection.QuerySingleOrDefaultAsync<PaginatedDataEntry>(sql, new { Id = id }))!;
    }


    /// <summary>
    /// Gets the estimated page count based off the total assets from a dataset/pageSz
    /// </summary>
    /// <param name="total"></param>
    /// <param name="pageSz"></param>
    /// <returns>total/pageSz + residual if pageSz does not evenly divide total</returns>
    private int CalculatePageCount(int total, int pageSz)
    {
        int pageCt = total / pageSz;
        pageCt+= total % pageSz == 0 ? 0 : 1;
        return pageCt;
    }

    /// <summary>
    /// Fetch a list of PaginatedDataEntry based on Id (pagination).
    /// </summary>
    /// <param name="page">page to fetch (1 indexed)</param>
    /// <param name="maxPageSize">total entries to include per page</param>
    /// <returns>Entries for the given page/pagesize, and the count of pages estimated by the filter</returns>
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

        return (entries, CalculatePageCount(totalCount,maxPageSize));
    }

    /// <summary>
    /// Fetches data created/updated after a given date (inclusive)
    /// </summary>
    /// <param name="date">data updated/added after (inclusive)</param>
    /// <param name="page">page to fetch (1 indexed)</param>
    /// <param name="maxPageSize">total entries to include per page</param>
    /// <returns>entries for the page, and an estimated max page countr</returns>
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

        return (entries, CalculatePageCount(totalCount,maxPageSize));
    }
}