using DataAccess.Poco;

namespace DataAccess;

public interface IPaginatedData
{
    /// <summary>
    /// Fetch a PaginatedDataEntry by Id.
    /// </summary>
    Task<PaginatedDataEntry> FetchByIdAsync(int id);

    /// <summary>
    /// Fetch a list of PaginatedDataEntry based on Id (pagination).
    /// </summary>
    Task<(IList<PaginatedDataEntry> Entries, int TotalCount)> FetchByIdAsync(int page, int maxPageSize);

    
    Task<(IList<PaginatedDataEntry> Entries, int TotalCount)> FetchByUpdatedOrCreatedAsync(DateTime after,
        int page, int maxPageSize);
}