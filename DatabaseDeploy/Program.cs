using System.Reflection;
using Microsoft.Data.SqlClient;

namespace DatabaseDeploy;

public class Program
{
    private const string EnvName = "PaginatedDataConnection";
    public static void Main(string[] args)
    {
        if (Environment.GetEnvironmentVariable(EnvName) == null)
        {
            Console.Error.WriteLine($"Environment Variable {EnvName} is not set");
            Environment.ExitCode = -1;
        }
        else
        {
            try
            {
                MigrateDatabase();
            }
            catch
            {
                Environment.ExitCode = -2;
            }
        }
        
        Environment.Exit(Environment.ExitCode);
    }
    
    private static void MigrateDatabase()
    {
        try
        {
            var sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(EnvName));
            var evolve = new EvolveDb.Evolve(sqlConnection, (msg)=>Console.WriteLine(msg))
            {
                EmbeddedResourceAssemblies = new[] { Assembly.GetExecutingAssembly() },
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error Deploying Database: {ex}");
            throw;
        }
    }

}