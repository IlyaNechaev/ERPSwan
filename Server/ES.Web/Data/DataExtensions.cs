using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PL.Web.Data;

public static class DataExtensions
{
    // Add SqlServer EFCore implementation
    public static void AddSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration
            .GetSection("ConnectionStrings")
            .GetChildren()
            .Select(n => n.Value);

        foreach (var connectionString in connectionStrings)
        {
            if (IsConnected(connectionString))
            {
                services.AddDbContext<ESDbContext>(builder =>
                {
                    builder.UseSqlServer(connectionString);
                });
                return;
            }
        }
    }

    // Проверка возможности подключения по connectionString
    private static bool IsConnected(string connectionString)
    {
        using var sqlConnection = new SqlConnection(connectionString);

        try
        {
            sqlConnection.Open();
        }
        catch(SqlException)
        {
            return false;
        }

        return true;
    }
}