using EvolveDb;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace AlchemistConsole.db;

public class DatabaseMigrationStartupFilter(IOptions<Configuration.Configuration> config) : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        try
        {
            using DbConnection context = new SqliteConnection(config.Value.ConnectionString);

            var evolve = new Evolve(context, Console.WriteLine)
            {
                Locations = ["db/migrations"],
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database migration: {ex.Message}");
            throw;
        }

        return next;
    }
}
