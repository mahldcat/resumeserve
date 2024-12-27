using Microsoft.Data.SqlClient;

namespace DataAccess;

public class PaginatedDataConnection(SqlConnection connection)
{
    public SqlConnection Connection { get; } = connection;
}

public class ResumeDataConnection(SqlConnection connection)
{
    public SqlConnection Connection { get; } = connection;
}
