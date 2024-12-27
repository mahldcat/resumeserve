using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccess;

public static class Extensions
{
    public const string ApphostPaginatedName = "PaginatedDataConnection";
    public const string ApphostResumeName = "ResumeDataConnection";
    
    public static IHostApplicationBuilder BuildPaginatedDataConnection(this IHostApplicationBuilder builder)
    {
        string paginatedConnectionString = builder.Configuration.GetConnectionString(ApphostPaginatedName)!;
        builder.Services.AddTransient<PaginatedDataConnection>(sp => new PaginatedDataConnection(new SqlConnection(paginatedConnectionString)));
        return builder;
    }
    public static IHostApplicationBuilder BuildResumeDataConnection(this IHostApplicationBuilder builder)
    {
        string paginatedConnectionString = builder.Configuration.GetConnectionString(ApphostResumeName)!;
        builder.Services.AddTransient<ResumeDataConnection>(sp => new ResumeDataConnection(new SqlConnection(paginatedConnectionString)));
        return builder;
    }

    /*
     string paginatedConnectionString = builder.Configuration.GetConnectionString("PaginatedDataConnection");
string resumeConnectionString = builder.Configuration.GetConnectionString("ResumeDataConnection");

// Register SqlConnection objects
builder.Services.AddTransient<SqlConnection>(sp => new SqlConnection(paginatedConnectionString));
builder.Services.AddTransient<SqlConnection>(sp => new SqlConnection(resumeConnectionString));

// Register named connections for better differentiation
builder.Services.AddTransient<PaginatedDataConnection>(sp => new PaginatedDataConnection(new SqlConnection(paginatedConnectionString)));
builder.Services.AddTransient<ResumeDataConnection>(sp => new ResumeDataConnection(new SqlConnection(resumeConnectionString)));

     */
    
}