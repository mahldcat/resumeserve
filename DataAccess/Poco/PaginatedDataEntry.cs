namespace DataAccess.Poco;

public class PaginatedDataEntry
{
    public int Id { get; set; }
    public string? Data { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}