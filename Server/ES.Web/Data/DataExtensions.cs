using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Data;

public static class DataExtensions
{
    const string ASPNET_CONNECTION = "ASPNET_DB_CONNECTION";

    // Регистрация сервиса SqlServer EFCore
    public static void AddSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var csType = Environment.GetEnvironmentVariable(ASPNET_CONNECTION) ?? "Default";

        var connectionString = configuration.GetConnectionString(csType);

        services.AddDbContext<ESDbContext>(builder =>
        {
            builder.UseSqlServer(connectionString);
        });
    }
}