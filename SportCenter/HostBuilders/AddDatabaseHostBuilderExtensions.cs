using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportCenter.DataAccess;

namespace SportCenter.HostBuilders;

public static class AddDatabaseHostBuilderExtensions
{
    public static IHostBuilder AddDatabase(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<DatabaseConnection>();
        });

        return host;
    }
}
